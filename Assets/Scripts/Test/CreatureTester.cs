using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreatureTester : MonoBehaviour {

    [Header("Creature Prefab")]
    public Creature creature;

    [Header("UI Objects")]
    public GameObject load;
    public InputField loadInputField;
    public GameObject deletion;
    public GameObject save;
    public GameObject store;
    public GameObject generation;
    public GameObject nameInput;
    public InputField nameInputField;
    public Text dataPath;

    [Header("Current Creature")]
    public Creature currentCreature;

    [Header("Loading")]
    public GameObject creaturePanel;
    public GameObject loadPanel;
    public RectTransform loadPanelContent;

    public void OpenLoadButton()
    {
        if (GameSave.current.creatures.Count != 0)
        {
            //get rid of old UI stuff
            List<GameObject> children = new List<GameObject>();
            for (int i = 0; i < loadPanelContent.transform.childCount; i++)
            {
                children.Add(loadPanelContent.transform.GetChild(i).gameObject);
            }
            for (int i = children.Count; i > 0; i--)
            {
                Destroy(children[i - 1]);
            }
            children.Clear();

            //create new UI
            load.SetActive(false);
            generation.SetActive(false);
            loadPanel.SetActive(true);
            for (int i = 0; i < GameSave.current.creatures.Count; i++)
            {
                loadPanelContent.GetComponent<RectTransform>().sizeDelta = new Vector2(loadPanelContent.GetComponent<RectTransform>().sizeDelta.x, 200 + (i * 200));
                GameObject c = Instantiate(creaturePanel);
                c.transform.SetParent(loadPanelContent.transform, false);
                c.transform.localPosition = new Vector2(0, 0);
                c.GetComponent<Creature_UI>().stats = GameSave.current.creatures[i];
                c.GetComponent<Creature_UI>().InitUI();
            }
        }
    }

    public void SaveCreature()
    {
        GameSave.current.creatures.Add(currentCreature.myStats);
        Destroy(currentCreature.gameObject);
        save.SetActive(false);
        deletion.SetActive(false);
        generation.SetActive(true);
        load.SetActive(true);
    }

    public void DeleteCreature()
    {
        GameSave.current.creatures.Remove(currentCreature.myStats);
        Destroy(currentCreature.gameObject);
        generation.SetActive(true);
        load.SetActive(true);
        deletion.SetActive(false);
        save.SetActive(false);
        store.SetActive(false);
    }

    public void StoreCreature()
    {
        Destroy(currentCreature.gameObject);
        store.SetActive(false);
        deletion.SetActive(false);
        generation.SetActive(true);
        load.SetActive(true);
    }

    /// <summary>
    /// creates a new random creature
    /// </summary>
	public void RandomCreature()
    {
        // deactivate UI elements
        load.SetActive(false);
        generation.SetActive(false);

        // instantiate new creature
        Creature newCreature = Instantiate(creature);
        newCreature.myStats = new Creature_Stats();

        // randomize variables
        newCreature.myStats.SetColors();
        //set special feature values

        // initialize prefab
        newCreature.Initialize();

        // set reference
        currentCreature = newCreature;

        // activate name input
        nameInput.SetActive(true);
    }

    /// <summary>
    /// takes user input and names creature
    /// </summary>
    public void NameInput()
    {
        // get input text and set name value
        currentCreature.myStats.creatureName = nameInputField.text;

        // reinitialize creature
        currentCreature.Initialize();

        // deactivate input UI
        nameInput.SetActive(false);

        // activate options
        deletion.SetActive(true);
        save.SetActive(true);
    }

    /*public void SaveCreatureOLD()
    {
        //SaveLoad.creatures.Add(currentCreature);
        SaveLoad.SaveCreature(currentCreature);
        Destroy(currentCreature.gameObject);
        save.SetActive(false);
        deletion.SetActive(false);
        generation.SetActive(true);
        load.SetActive(true);
        dataPath.text = Application.persistentDataPath;
    }

    public void DeleteCreatureOLD()
    {
        Destroy(currentCreature.gameObject);
        generation.SetActive(true);
        load.SetActive(true);
        deletion.SetActive(false);
        save.SetActive(false);
    }

    public void LoadCreatureOLD()
    {
        Creature_Stats stats = SaveLoad.LoadCreature(loadInputField.text);
        Creature loadedCreature = Instantiate(creature);
        loadedCreature.myStats = stats;
        loadedCreature.Initialize();
        currentCreature = loadedCreature;
        dataPath.text = Application.persistentDataPath;
        generation.SetActive(false);
        load.SetActive(false);
        deletion.SetActive(true);
        save.SetActive(true);
    }*/
}
