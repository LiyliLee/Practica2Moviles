using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [SerializeField]
    private CategotyLevel[] categories;

    private int categoryToPlay;
    private int packToPlay;
    private int levelToPlay;
    private int[] packLevelsUnlocked;

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

    // Start is called before the first frame update
    void Start()
    {
        int totalPacks = 0;
        for (int i =0; i< categories.Length; i++)
        {
            totalPacks += categories[i].packs.Length;
        }
        packLevelsUnlocked = new int[totalPacks];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
