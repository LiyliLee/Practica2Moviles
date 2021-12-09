using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
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
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        packLevelsUnlocked = new int[categories.Length][];
        for (int i =0; i< categories.Length; i++)
        {
            packLevelsUnlocked[i] = new int[categories[i].packs.Length];
        }
        

        //grid_.createLevel(categories[0].packs[0].levels, 1);
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

    public Grid grid_;

    [SerializeField]
    private CategotyLevel[] categories;

    private int categoryToPlay;
    private int packToPlay;
    private int levelToPlay;
    private int[][] packLevelsUnlocked;


    public CategotyLevel[] GetCategoties() { return categories; }
    public int GetPackunlockeds(int catid, int packid) { return packLevelsUnlocked[catid][packid]; }
}
