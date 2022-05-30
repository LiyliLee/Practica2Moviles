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
            levelManager.SetLevel(_categories[2].packs[2].levels, 5, _categories[2].color);

            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            _instance.levelManager = levelManager;
            _instance._menuManager = _menuManager;
            _instance.gridManager = gridManager;

            levelManager.SetLevel(_categories[categoryToPlay].packs[packToPlay].levels, levelToPlay, _categories[categoryToPlay].color);

            DestroyImmediate(gameObject);
            return;
        }

    }

    public static GameManager GetInstance()
    {
        return _instance;
    }

    public CategoryLevel[] GetCategories() { return _categories; }
    public int GetPackUnlockeds(int catid, int packid) { return packLevelsUnlocked[catid][packid]; }

    private void OnApplicationQuit()
    {
        DataSaver.SavePlayerData(_player);
    }

    public PlayerData GetPlayerData() {
        return _player;
    }

    public CategoryLevel GetCategory()
    {
        return _categories[categoryToPlay];
    }

    public PackLevel GetPack()
    {
        return _categories[categoryToPlay].packs[packToPlay];
    }

    public int GetLevelMoves()
    {
        return GetPlayerData()._moves[GetPack().packName][levelToPlay];
    }

    public void SetLevelMoves()
    {
        if(GetLevelMoves() == 0 || GetLevelMoves() > levelManager.GetSteps())
            GetPlayerData()._moves[GetPack().packName][levelToPlay] = levelManager.GetSteps();
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
            levelManager.SetLevel(_categories[2].packs[0].levels, 5, _categories[2].color);

        }
        else if (_instance._menuManager != null)
        {
            // se carga menu
            _instance._menuManager.Init(_categories[categoryToPlay], _levelPack, _categories, _player._passedLevelInfo);
        }
    }

    public void AddHint()
    {
        _player._hints++;
    }

    public void DecreaseHint()
    {
        _player._hints--;
    }   

    public int GetHints()
    {
        return _player._hints;
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
        if(levelToPlay < 149)
        {
            levelToPlay += 1;
            LoadLevelScene();
        }
    }

    public void LoadPreviousLevel()
    {
        if (levelToPlay > 0)
        {
            levelToPlay -= 1;
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

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            DataSaver.SavePlayerData(_player);
        }
    }
}
