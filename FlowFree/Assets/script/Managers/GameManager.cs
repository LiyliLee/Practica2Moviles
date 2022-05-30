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

    public string _menuSceneName = "MenuScene";
    public string _levelSceneName = "LevelScene";

    [SerializeField]
    private int _categoryToPlay;
    private int _packToPlay;
    private int _levelToPlay;
    private int levelsInPack_;

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
            _instance.SetupScene();

            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            _instance.levelManager = levelManager;
            _instance._menuManager = _menuManager;
            _instance.gridManager = gridManager;

            _instance.SetupScene();

            DestroyImmediate(gameObject);
        }

    }

    public static GameManager GetInstance()
    {
        return _instance;
    }

    public CategoryLevel[] GetCategories() { return _categories; }

    public PlayerData GetPlayerData() {
        return _player;
    }

    public CategoryLevel GetCategory()
    {
        return _categories[_categoryToPlay];
    }

    public PackLevel GetPack()
    {
        return _categories[_categoryToPlay].packs[_packToPlay];
    }

    public int GetLevelMoves()
    {
        return GetPlayerData()._moves[GetPack().packName][_levelToPlay];
    }

    public void SetLevelMoves()
    {
        if(GetLevelMoves() == 0 || GetLevelMoves() > levelManager.GetSteps())
            GetPlayerData()._moves[GetPack().packName][_levelToPlay] = levelManager.GetSteps();
    }

    public void NextLevel()
    {
        if (levelsInPack_ > _levelToPlay + 1)
        {
            _levelToPlay++;
            ToLevelScene();
        }
        else ToMenuScene();
    }

    public void PrevLevel()
    {
        if(_levelToPlay > 0)
        {
            _levelToPlay--;
            ToLevelScene();
        }
    }

    public void ToLevelScene()
    {
        SceneManager.LoadScene(_levelSceneName);
    }
    public void ToMenuScene()
    {
        _fromLevelScene = true;
        SceneManager.LoadScene(_menuSceneName);
    }

    public string GetPackName()
    {
        return _categories[_categoryToPlay].packs[_packToPlay].packName;
    }

    public int GetLevelToPlay()
    {
        return _levelToPlay;
    }

    public void SetLevelToPlay(int level)
    {
        _levelToPlay = level;
    }

    public void SetCategoryToPlay(int category)
    {
        _categoryToPlay = category;
    }

    public void SetPackToPlay(int pack)
    {
        _packToPlay = pack;
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
            levelManager.SetLevel(_categories[_categoryToPlay].packs[_packToPlay].levels,
                _levelToPlay, _categories[_categoryToPlay].color);

        }
        else if (_instance._menuManager != null)
        {
            // se carga menu
            _instance._menuManager.Init(_categories[_categoryToPlay], _categories[_categoryToPlay].packs[_packToPlay],
                _categories, _player._passedLevelInfo);
        }
    }

    public void AddHint()
    {
        _player._hints++;
        levelManager.SetHintText();
    }

    public void DecreaseHint()
    {
        _player._hints--;
    }

    public void SelectedLevelInfo(string categoryToPlay, string packToPlay, int levelToPlay)
    {
        for (int i = 0; i < _categories.Length; i++)
        {
            if (_categories[i].name == categoryToPlay)
            {
                _categoryToPlay = i;
                for (int j = 0; j < _categories[i].packs.Length; j++)
                {
                    if (_categories[i].packs[j].packName == packToPlay)
                        _packToPlay = j;
                }
            }
            _levelToPlay = levelToPlay;
        }
    }

    public int GetHints()
    {
        return _player._hints;
    }

    public PlayerData.PassedLevelInfo GetPassedLevelInfo()
    {
        return _player._passedLevelInfo[GetPack().packName][_levelToPlay];
    }

    public void SetPassedLevelInfo()
    {
        if (levelManager.GetSteps() == levelManager.GetLevelData().numFlows)
            _player._passedLevelInfo[GetPack().packName][_levelToPlay] = PlayerData.PassedLevelInfo.PERFECT;
        else _player._passedLevelInfo[GetPack().packName][_levelToPlay] = PlayerData.PassedLevelInfo.PASSED;
    }

    public void SaveLevel()
    {
        SetPassedLevelInfo();
        SetLevelMoves();
        DataSaver.SavePlayerData(_player);
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

    private void OnApplicationQuit()
    {
        DataSaver.SavePlayerData(_player);
    }
}
