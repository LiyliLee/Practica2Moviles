using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public Grid grid_;

    [SerializeField]
    private CategotyLevel[] categories_;

    private int categoryToPlay;
    private int packToPlay;
    private int levelToPlay;
    private int[][] packLevelsUnlocked;

    private PlayerData player_;


    public static GameManager _instance;
    void Awake()
    {
        if (_instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        else
        {
            _instance = this;


            List<string> packNames = new List<string>();

            for(int i = 0; i < categories_.Length; i++)
            {
                foreach(PackLevel pack in categories_[i].packs)
                {
                    packNames.Add(pack.packName);
                }
            }

            player_ = DataSaver.LoadPlayerData(packNames);

            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        /*packLevelsUnlocked = new int[categories_.Length][];
        for (int i =0; i< categories_.Length; i++)
        {
            packLevelsUnlocked[i] = new int[categories_[i].packs.Length];

        }*/
        
        grid_.createLevel(categories_[0].packs[0].levels, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameManager GetInstance()
    {
        return _instance;
    }

    public void ProcessInput(InputManager.MoveType move, Vector2 pos)
    {
        grid_.ProcessInput(move, pos);
    }

    public CategotyLevel[] GetCategoties() { return categories_; }
    public int GetPackunlockeds(int catid, int packid) { return packLevelsUnlocked[catid][packid]; }

    private void OnApplicationQuit()
    {
        DataSaver.SavePlayerData(player_);
    }

    public PlayerData GetPlayerData() {
        return GetInstance().player_;
    }
}
