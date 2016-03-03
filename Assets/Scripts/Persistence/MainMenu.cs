using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {


    [Header("--- New Game ---")]
    public GameObject newGame;
    public GameObject nameInput;
    public InputField nameInputField;

    [Header("--- Load Game ---")]
    public GameObject loadGame;
    public GameObject loadPanel;
    public GameObject loadPanelContent;
    public Button loadButton;
    public GameObject buttonPrefab;

    [Header("--- Misc ---")]
    public int levelIndex;

    void Awake()
    {
        newGame.SetActive(true);
        loadGame.SetActive(true);
        SavedGames.currentSaves = new SavedGames();
        SavedGames.currentSaves.saves = new List<GameSave> { };
        SaveLoad.LoadGameSaves();
        if (SavedGames.currentSaves == null || SavedGames.currentSaves.saves == null || SavedGames.currentSaves.saves.Count == 0)
        {
            loadButton.interactable = false;
            loadButton.image.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    public void NewGame()
    {
        GameSave.current = new GameSave();
        nameInput.gameObject.SetActive(true);
        loadGame.SetActive(false);
        newGame.SetActive(false);
    }

    public void NameInput()
    {
        GameSave.current.playerName = nameInputField.text;
        SavedGames.currentSaves.saves.Add(GameSave.current);
        SaveLoad.SaveGameSaves();
        Application.LoadLevel(levelIndex);
    }

    public void LoadGame()
    {
        loadPanel.SetActive(true);
        for (int i = 0; i < SavedGames.currentSaves.saves.Count; i++)
        {
            loadPanelContent.GetComponent<RectTransform>().sizeDelta = new Vector2(loadPanelContent.GetComponent<RectTransform>().sizeDelta.x, 50 + (i * 50));
            GameObject b = Instantiate(buttonPrefab);
            b.transform.SetParent(loadPanelContent.transform, false);
            b.transform.localPosition = new Vector2(0, (35* i));
            b.transform.GetChild(0).GetComponent<Text>().text = SavedGames.currentSaves.saves[i].playerName;
            b.GetComponent<LoadSaveButton>().save = SavedGames.currentSaves.saves[i];
        }
        loadGame.SetActive(false);
        newGame.SetActive(false);
    }

}
