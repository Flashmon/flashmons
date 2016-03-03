using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{

    public static void LoadGameSaves()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesaves.fmns"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesaves.fmns", FileMode.Open);
            SavedGames s = (SavedGames)bf.Deserialize(file);
            file.Close();
            if (s.saves != null && s.saves.Count > 0)
            {
                Debug.Log("loading saves");
                SavedGames.currentSaves.saves = s.saves;
            }
            else
            {
                Debug.Log("no saves to load");
            }
        }
    }

    public static void SaveGameSaves()
    {
        SavedGames saves = SavedGames.currentSaves;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesaves.fmns");
        bf.Serialize(file, saves);
        file.Close();
    }

    public static void SaveCreature(Creature c)
    {
        Creature_Stats creature = c.myStats;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/"+creature.creatureName+".creature");
        bf.Serialize(file, creature);
        file.Close();
    }

    public static Creature_Stats LoadCreature(string s)
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
