using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public MenuManager _menuManager;
    public LevelManager levelManager;
    public GridManager gridManager;

    public CategoryLevel[] categories_;

    [SerializeField]
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
            gridManager.CreateLevel(levelManager.CreateLevel(categories_[categoryToPlay].packs[packToPlay].levels, levelToPlay));

            DestroyImmediate(gameObject);
            return;
        }
        else
        {
            _instance = this;

            /*List<string> packNames = new List<string>();

            for(int i = 0; i < categories_.Length; i++)
            {
                foreach(PackLevel pack in categories_[i].packs)
                {
                    packNames.Add(pack.packName);
                }
            }

            player_ = DataSaver.LoadPlayerData(packNames);

            levelManager.SetLevel(categories_[0].packs[0].levels, 2);*/

            SetupScene();

            DontDestroyOnLoad(gameObject);
        }
    }

    public static GameManager GetInstance()
    {
        return _instance;
    }

    public void ProcessInput(InputManager.MoveType move, Vector2 pos)
    {
        //grid_.ProcessInput(move, pos);
    }

    public CategoryLevel[] GetCategories() { return categories_; }
    public int GetPackUnlockeds(int catid, int packid) { return packLevelsUnlocked[catid][packid]; }

    private void OnApplicationQuit()
    {
        //DataSaver.SavePlayerData(player_);
    }

    public PlayerData GetPlayerData() {
        return GetInstance().player_;
    }

    private void SetupScene()
    {
        if (_instance.levelManager != null)
        {
            // se carga nivel
            levelManager.SetLevel(categories_[0].packs[0].levels, 2);

        }
        else if (_instance._menuManager != null)
        {
            // se carga menu
            _instance._menuManager.Init(categories_);
        }
    }
}
