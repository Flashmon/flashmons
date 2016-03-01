using UnityEngine;
using System.Collections;

[System.Serializable]
public class Creature_Stats {

    public string creatureName;

    public int health;
    public int attack;
    public int defense;
    public int speed;

    public float[] baseColor;
    public float[] tintColor;
    public float[] eyeColor;

    public int[] specialFeatures;

    public void SetColors()
    {
        baseColor = RandomColor();
        tintColor = RandomColor();
        eyeColor = RandomColor();
    }

    public float[] RandomColor()
    {
        return new float[3] { RandomColorValue(), RandomColorValue(), RandomColorValue() };
    }

    public float RandomColorValue()
    {
        return Random.Range(0, 255);
    }

}
