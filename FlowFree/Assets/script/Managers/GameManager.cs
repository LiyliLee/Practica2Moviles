using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using System;

public class GameManager : MonoBehaviour
{
    public MenuManager _menuManager;
    public LevelManager levelManager;
    public GridManager gridManager;

    public CategoryLevel[] _categories;

    public string _menuSceneName = "Menu";
    public string _levelSceneName = "LevelScene";

    [SerializeField]
    private int categoryToPlay;
    private int packToPlay;
    private int levelToPlay;
    private int levelsInPack_;
    private int[][] packLevelsUnlocked;
    private PackLevel _levelPack;

    private PlayerData _player;
    private bool _fromLevelScene = false;

    // Singleton
    private static GameManager _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            List<string> packNames = new List<string>();

            for (int i = 0; i < _categories.Length; i++)
            {
                foreach (PackLevel pack in _categories[i].packs)
                {
                    packNames.Add(pack.packName);
                }
            }

            _player = DataSaver.LoadPlayerData(packNames);

            _instance.CreateScene();

            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            _instance.levelManager = levelManager;
            _instance._menuManager = _menuManager;
            _instance.gridManager = gridManager;

            gridManager.CreateLevel(levelManager.CreateLevel(_categories[_categoryToPlay].packs[_packToPlay].levels, _levelToPlay));

            DestroyImmediate(gameObject);
            return;
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
            _instance._menuManager.Init(_categories[_categoryToPlay], _levelPack, _categories, _player._passedLevelInfo);
        }
    }

    public PlayerData GetPlayerData() {
        return _instance._player;
    }

    public void AddHint()
    {
        _player._hints++;
    }

    public void DecreaseHint()
    {
        _player._hints--;
    }   

    public void LoadLevelScene()
    {
        Debug.Log("Loading Level");
        SceneManager.LoadScene(_levelSceneName);
    }

    public void LoadMenu()
    {
        _fromLevelScene = true;
        SceneManager.LoadScene(_menuSceneName);
    }

    public void LoadNextLevel()
    {
        if(_levelToPlay < 149)
        {
            _levelToPlay += 1;
            LoadLevelScene();
        }
    }

    public void LoadPreviousLevel()
    {
        if (_levelToPlay > 0)
        {
            _levelToPlay -= 1;
            LoadLevelScene();
        }
    }

    public bool FromLevelScene()
    {
        if (_fromLevelScene)
        {
            _fromLevelScene = false;
            return true;
        }
        else return false;
    }

    private void OnApplicationQuit()
    {
        DataSaver.SavePlayerData(_player);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            DataSaver.SavePlayerData(_player);
        }
    }
}
