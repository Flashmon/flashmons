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

    public void InitUI()
    {
        eye.color = new Color(stats.eyeColor[0] / 255, stats.eyeColor[1] / 255, stats.eyeColor[2] / 255);
        for (int i = 0; i < baseImages.Length; i++)
        {
            baseImages[i].color = new Color(stats.baseColor[0]/255, stats.baseColor[1] / 255, stats.baseColor[2] / 255);
            tintImages[i].color = new Color(stats.tintColor[0/255] / 255, stats.tintColor[1] / 255, stats.tintColor[2] / 255);
        }
        tester = GameObject.FindObjectOfType<CreatureTester>();
        nameText.text = stats.creatureName;
    }

    public void LoadCreature()
    {
        Creature loadedCreature = Instantiate(tester.creature);
        loadedCreature.myStats = stats;
        loadedCreature.Initialize();
        tester.currentCreature = loadedCreature;
        tester.deletion.SetActive(true);
        tester.store.SetActive(true);
        tester.loadPanel.SetActive(false);
    }
}
