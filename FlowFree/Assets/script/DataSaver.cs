using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public struct PlayerData
{
    public int level_;
    public Dictionary<string, int[]> completedLotLevel_;
    public int hints_;
    public bool premium_;

    public PlayerData(int level, Dictionary<string, int[]> completed, int hints, bool premium)
    {
        level_ = level;
        completedLotLevel_ = completed;
        hints_ = hints;
        premium_ = premium;
    }
}

public class DataSaver
{
    public static void SavePlayerData(PlayerData playerData)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/flowFreeData.dat");

        bf.Serialize(file, playerData);

        file.Close();
    }

    public static PlayerData ResetData(List<string> packs)
    {
        Dictionary<string, int[]> completed = new Dictionary<string, int[]>();

        for(int i = 0; i < packs.Count; i++)
        {
            int[] levels = new int[150];

            completed.Add(packs[i], levels);
        }

        return new PlayerData(0, completed, 0, false);

    }
    public static PlayerData LoadPlayerData(List<string> packs)
    {
        // si existen datos carga el archivo
        if (File.Exists(Application.persistentDataPath + "/flowFreeData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/flowFreeData.dat", FileMode.Open);

            PlayerData playerData = (PlayerData)bf.Deserialize(file);

            return playerData;
        }
        // si no crea uno nuevo a partir de los niveles completados por pack
        else return ResetData(packs);
    }
}
