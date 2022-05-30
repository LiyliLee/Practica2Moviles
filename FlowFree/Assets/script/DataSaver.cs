using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public struct PlayerData
{
    public enum PassedLevelInfo { NO, PASSED, PERFECT };

    public Dictionary<string, PassedLevelInfo[]> _passedLevelInfo; // niveles completados por pack
    public Dictionary<string, int[]> _moves; // numero de movimientos por nivel
    public int _hints; // numero de pistas
    public bool _premium; // usuario sin anuncios

    public PlayerData(Dictionary<string, PassedLevelInfo[]> passed, Dictionary<string, int[]> moves, int hints, bool premium)
    {
        _passedLevelInfo = passed;
        _moves = moves;
        _hints = hints;
        _premium = premium;
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
        Dictionary<string, PlayerData.PassedLevelInfo[]> completed = new Dictionary<string, PlayerData.PassedLevelInfo[]>();
        Dictionary<string, int[]> moves = new Dictionary<string, int[]>();

        for (int i = 0; i < packs.Count; i++)
        {
            completed.Add(packs[i], new PlayerData.PassedLevelInfo[150]);
            moves.Add(packs[i], new int[150]);
        }

        return new PlayerData(completed, moves, 0, false);

    }
    public static PlayerData LoadPlayerData(List<string> packs)
    {
        // si existen datos carga el archivo
        if (File.Exists(Application.persistentDataPath + "/flowFreeData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/flowFreeData.dat", FileMode.Open);

            PlayerData playerData = (PlayerData)bf.Deserialize(file);

            file.Close();

            if (playerData._passedLevelInfo == null || playerData._moves == null)
            {
                if (playerData._passedLevelInfo == null)
                    playerData._passedLevelInfo = new Dictionary<string, PlayerData.PassedLevelInfo[]>();

                if (playerData._moves == null)
                    playerData._moves = new Dictionary<string, int[]>();

                for(int i = 0; i < packs.Count; i++)
                {
                    playerData._passedLevelInfo.Add(packs[i], new PlayerData.PassedLevelInfo[150]);
                    playerData._moves.Add(packs[i], new int[150]);
                }
            }

            return playerData;
        }
        // si no crea uno nuevo a partir de los niveles completados por pack
        else return ResetData(packs);
    }
}
