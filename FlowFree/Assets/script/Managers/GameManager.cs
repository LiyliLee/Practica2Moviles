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

            _instance.CreateScene();

            DestroyImmediate(gameObject);
        }

    }

    public static GameManager GetInstance()
    {
        return _instance;
    }

    private void CreateScene()
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

    public void LoadLevelScene()
    {
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
