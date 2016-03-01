using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

    [Header("Name")]
    public string creatureName;

    [Header("Stats")]
    private int health;
    public int attack;
    public int defense;
    public int speed;

    [Header("Current HP")]
    public int currentHealth;

    [Header("Color")]
    public Color baseColor;
    public Color tintColor;
    public Color eyeColor;

    [Header("Special Feature Indexes")]
    public int[] specialFeatures;

    public Creature_Stats myStats;
    public Creature_GameObjects myGameObjects;
    public Creature_Attack[] myAttacks;

    public void Initialize()
    {
        // get values from stats object
        creatureName = myStats.creatureName;

        health = myStats.health;
        attack = myStats.attack;
        defense = myStats.defense;
        speed = myStats.speed;

        baseColor = new Color(myStats.baseColor[0]/255, myStats.baseColor[1] / 255, myStats.baseColor[2] / 255);
        tintColor = new Color(myStats.tintColor[0] / 255, myStats.tintColor[1] / 255, myStats.tintColor[2] / 255);
        eyeColor = new Color(myStats.eyeColor[0] / 255, myStats.eyeColor[1] / 255, myStats.eyeColor[2] / 255);

        specialFeatures = myStats.specialFeatures;

        // setup sprites

        for (int i = 0; i < myGameObjects.baseSpritesRaising.Length; i++)
        {
            myGameObjects.baseSpritesRaising[i].color = baseColor;
        }
        for (int i = 0; i < myGameObjects.tintSpritesRaising.Length; i++)
        {
            myGameObjects.tintSpritesRaising[i].color = tintColor;
        }
        for (int i = 0; i < myGameObjects.eyeSpritesRaising.Length; i++)
        {
            myGameObjects.eyeSpritesRaising[i].color = eyeColor;
        }
        for (int i = 0; i < myGameObjects.baseSpritesBattle.Length; i++)
        {
            myGameObjects.baseSpritesBattle[i].color = baseColor;
        }
        for (int i = 0; i < myGameObjects.tintSpritesBattle.Length; i++)
        {
            myGameObjects.tintSpritesBattle[i].color = tintColor;
        }
        for (int i = 0; i < myGameObjects.eyeSpritesBattle.Length; i++)
        {
            myGameObjects.eyeSpritesBattle[i].color = eyeColor;
        }
    }

    Color randomColor()
    {
        return new Color(((float)(Random.Range(0, 255)))/255f, ((float)(Random.Range(0, 255))) / 255f, ((float)(Random.Range(0, 255))) / 255f);
    }
}
