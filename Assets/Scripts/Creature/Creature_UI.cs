using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Creature_UI : MonoBehaviour {

    public Image[] baseImages;
    public Image[] tintImages;
    public Image eye;

    public Text nameText;

    public Creature_Stats stats;

    public CreatureTester tester;
    public Battle_Manager battleManager;

    public void InitUI()
    {
        eye.color = new Color(stats.eyeColor[0] / 255, stats.eyeColor[1] / 255, stats.eyeColor[2] / 255);
        for (int i = 0; i < baseImages.Length; i++)
        {
            baseImages[i].color = new Color(stats.baseColor[0]/255, stats.baseColor[1] / 255, stats.baseColor[2] / 255);
            tintImages[i].color = new Color(stats.tintColor[0/255] / 255, stats.tintColor[1] / 255, stats.tintColor[2] / 255);
        }
        if (FindObjectOfType<CreatureTester>())
        {
            tester = FindObjectOfType<CreatureTester>();
        }
        if (FindObjectOfType<Battle_Manager>())
        {
            battleManager = FindObjectOfType<Battle_Manager>();
        }
        nameText.text = stats.creatureName;
    }

    public void LoadCreatureUI()
    {
        Creature loadedCreature = Instantiate(tester.creature);
        loadedCreature.myStats = stats;
        loadedCreature.Initialize();
        tester.currentCreature = loadedCreature;
        tester.deletion.SetActive(true);
        tester.store.SetActive(true);
        tester.loadPanel.SetActive(false);
        loadedCreature.myGameObjects.battleSprites.SetActive(false);
        loadedCreature.myGameObjects.raisingSprites.SetActive(true);
    }

    public void LoadCreatureBattle()
    {
        Creature loadedCreature = Instantiate(battleManager.creaturePrefab).GetComponent<Creature>();
        loadedCreature.myStats = stats;
        loadedCreature.Initialize();
        if (battleManager.creatureA == null)
        {
            battleManager.creatureA = loadedCreature;
            battleManager.creatureA.transform.position = battleManager.creatureAStartPos;
            battleManager.creatureA.transform.localScale = new Vector3(-1, 1, 1);
            loadedCreature.myGameObjects.battleSprites.SetActive(true);
            loadedCreature.myGameObjects.raisingSprites.SetActive(false);
        }
        else if (battleManager.creatureB == null)
        {
            battleManager.creatureB = loadedCreature;
            battleManager.creatureB.transform.position = battleManager.creatureBStartPos;
            loadedCreature.myGameObjects.battleSprites.SetActive(true);
            loadedCreature.myGameObjects.raisingSprites.SetActive(false);
            battleManager.StartBattle();
        }
    }
}
