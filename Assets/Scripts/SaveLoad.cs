using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{

    public static List<Creature> creatures;

    //public static Creature_Stats currentStats;

    public static void Save()
    {
        //HighScoresManager.highScores = UserScore.current;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, SaveLoad.creatures);
        file.Close();
    }

    public static void Save(Creature c)
    {
        Creature_Stats creature = c.myStats;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/"+creature.creatureName+".creature");
        bf.Serialize(file, creature);
        file.Close();
    }

    public static Creature_Stats Load(string s)
    {
        if (File.Exists(Application.persistentDataPath + "/" + s + ".creature"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + s + ".creature", FileMode.Open);
            Creature_Stats c = (Creature_Stats)bf.Deserialize(file);
            file.Close();
            File.Delete(Application.persistentDataPath + "/" + s + ".creature");
            return c;
        }
        else
        {
            return null;
        }
    }

}
