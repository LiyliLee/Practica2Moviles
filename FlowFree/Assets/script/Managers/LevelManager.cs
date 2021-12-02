using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public struct LevelData
    {
        public int width, height;
        public int numFlows;

        public int[] bridges_;
        public int[] emptys_;
        public Vector2[] wall_;

        public List<int>[] solutions_;
    }

    public LevelData CreateLevel(TextAsset pack, int levelToPlay)
    {
        LevelData levelData = new LevelData();

        string packText = pack.text;

        string[] levels = packText.Split('\n');

        string[] level = levels[levelToPlay].Split(';');
        string[] cabecera = level[0].Split(',');

        //Cabecera
        string tamanho = cabecera[0];

        string[] tam = tamanho.Split(':');
        if(tam.Length == 1)
        {
            levelData.width = int.Parse(tam[0]);
            levelData.height = int.Parse(tam[0]);
        } else {
            levelData.width = int.Parse(tam[0]);
            levelData.height = int.Parse(tam[1]);
        }

        levelData.numFlows = int.Parse(cabecera[3]);

        if(cabecera.Length > 4)
        {
            string[] bridges = level[4].Split(':');
            levelData.bridges_ = new int[bridges.Length];
            for(int i = 0; i < bridges.Length; i++)
            {
                levelData.bridges_[i] = int.Parse(bridges[i]);
            }
        }
        if (cabecera.Length > 5)
        {
            string[] emptys = level[5].Split(':');
            levelData.emptys_ = new int[emptys.Length];
            for (int i = 0; i < emptys.Length; i++)
            {
                levelData.emptys_[i] = emptys[i][0] - '0';
            }
        }
        if (cabecera.Length > 6)
        {
            string[] walls = level[6].Split(':');
            levelData.wall_ = new Vector2[walls.Length];
            for (int i = 0; i < walls.Length; i++)
            {
                string[] wallaux = walls[i].Split('|');
                levelData.wall_[i].x = int.Parse(wallaux[0]);
                levelData.wall_[i].y = int.Parse(wallaux[1]);
            }
        }

        levelData.solutions_ = new List<int>[levelData.numFlows];
        //Flows
        for (int i = 0; i < levelData.numFlows; i++)
        {
            string solution = level[i + 1];
            string[] casillas = solution.Split(',');
            levelData.solutions_[i] = new List<int>();
            for(int j = 0; j < casillas.Length;j++)
            {
                levelData.solutions_[i].Add(int.Parse(casillas[j]));
            }
        }

        return levelData;
    }
}
