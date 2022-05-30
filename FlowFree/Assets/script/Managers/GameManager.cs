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
    private int levelsInPack_;
    private int[][] packLevelsUnlocked;

    private PlayerData player_;

    public static GameManager _instance;

    void Awake()
    {
        if (_instance != null)
        {
            levelManager.SetLevel(categories_[categoryToPlay].packs[packToPlay].levels, levelToPlay, categories_[categoryToPlay].color);

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

            player_ = DataSaver.LoadPlayerData(packNames);*/

            SetupScene();

            DontDestroyOnLoad(gameObject);
        }
    }

    public static GameManager GetInstance()
    {
        return _instance;
    }

    public CategoryLevel[] GetCategories() { return categories_; }
    public int GetPackUnlockeds(int catid, int packid) { return packLevelsUnlocked[catid][packid]; }

    private void OnApplicationQuit()
    {
        DataSaver.SavePlayerData(player_);
    }

    public PlayerData GetPlayerData() {
        return GetInstance().player_;
    }

    public void AddHint()
    {
        levelManager.AddHintNum(1);
    }

    public void NextLevel()
    {
        if (levelsInPack_ > levelToPlay + 1)
        {
            levelToPlay++;
            ToLevelScene();
        }
        else ToMenuScene();
    }

    public void PrevLevel()
    {
        if(levelToPlay > 0)
        {
            levelToPlay--;
            ToLevelScene();
        }
    }

    public void ToLevelScene()
    {
        SceneManager.LoadScene(1);
    }
    public void ToMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void SetLevelToPlay(int level)
    {
        levelToPlay = level;
    }

    public void SetCategoryToPlay(int category)
    {
        categoryToPlay = category;
    }

    public void SetPackToPlay(int pack)
    {
        packToPlay = pack;
    }

    public void SetLevelsInPack(int levels)
    {
        levelsInPack_ = levels;
    }

    private void SetupScene()
    {
        if (_instance.levelManager != null)
        {
            // se carga nivel
            levelManager.SetLevel(categories_[2].packs[0].levels, 5, categories_[2].color);

        }
        else if (_instance._menuManager != null)
        {
            // se carga menu
            _instance._menuManager.Init(categories_);
        }
    }
}
