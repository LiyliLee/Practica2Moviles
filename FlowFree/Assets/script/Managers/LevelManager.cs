using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        //Puentes
        if(cabecera.Length > 4)
        {
            string[] bridges = level[4].Split(':');
            levelData.bridges_ = new int[bridges.Length];
            for(int i = 0; i < bridges.Length; i++)
            {
                levelData.bridges_[i] = int.Parse(bridges[i]);
            }
        }
        //Huecos
        if (cabecera.Length > 5)
        {
            string[] emptys = level[5].Split(':');
            levelData.emptys_ = new int[emptys.Length];
            for (int i = 0; i < emptys.Length; i++)
            {
                levelData.emptys_[i] = emptys[i][0] - '0';
            }
        }
        //Paredes
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
    public void SetLevel(TextAsset pack, int level)
    {
        levelData_ = CreateLevel(pack, level);
        gridManager.CreateLevel(levelData_);
        SetFlowsText(0, levelData_.numFlows);
        SetPipeText(0, 1);
        SetMovesText(0, levelData_.numFlows);
        SetHintText(3);
    }

    public void Win()
    {
        finishPanel_.SetActive(true);
        finishPanel_.GetComponent<FinishPanel>().SetFinishPanel(gridManager.getSteps() == gridManager.getNumFlows(), gridManager.getSteps());
        canPlay_ = false;
    }

    public void SetPlay(bool aux)
    {
        canPlay_ = aux;
    }

    public void SetPipeText(int count, int maxCount)
    {
        float percentage = Mathf.Round(((float)count / (float)maxCount)*100);
        pipeText.text = "tuber�a: " + percentage + "%";
    }

    public void SetFlowsText(int completed, int total)
    {
        flowsText.text = "flujos: " + completed + "/" + total;
    }

    public void SetMovesText(int moves, int best)
    {
        movesText.text = "pasos: " + moves + " r�cord: " + best;
    }

    public void SetHintText(int hints)
    {
        hintText.text = hints + " x";
    }

    public bool GetCanPlay()
    {
        return canPlay_;
    }

    LevelData levelData_;
    public GridManager gridManager;

    public GameObject finishPanel_;

    bool canPlay_ = true;

    // Textos de progresion de nivel
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI flowsText;
    public TextMeshProUGUI movesText;
    public TextMeshProUGUI pipeText;
    public TextMeshProUGUI hintText;

}
