using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameSave {

    public static GameSave current;

    public List<Creature_Stats> creatures;
    public List<Creature_Stats> genes;

    public float money;

    public string playerName;

    public GameSave()
    {
        creatures = new List<Creature_Stats> { };
        genes = new List<Creature_Stats> { };
    }
}
