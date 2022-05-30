using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        GameManager.GetInstance().SetLevelsInPack(levels.Length);

        string[] level = levels[levelToPlay].Split(';');
        string[] cabecera = level[0].Split(',');

        //Cabecera
        string tamanho = cabecera[0];

        string[] tam = tamanho.Split(':');
        if (tam.Length == 1)
        {
            levelData.width = int.Parse(tam[0]);
            levelData.height = int.Parse(tam[0]);
        } else {
            levelData.width = int.Parse(tam[0]);
            if (tam[1].Length < 2)
                levelData.height = int.Parse(tam[1]);
            else
            {
                if (tam[1].Length == 3)
                    levelData.height = int.Parse(tam[1][0].ToString());
                else levelData.height = int.Parse((tam[1][0].ToString() + tam[1][0].ToString()));
            }
        }

        levelData.numFlows = int.Parse(cabecera[3]);

        //Puentes
        if (cabecera.Length > 4)
        {
            if (cabecera[4] != "")
            {
                string[] bridges = cabecera[4].Split(':');
                levelData.bridges_ = new int[bridges.Length];
                for (int i = 0; i < bridges.Length; i++)
                {
                    levelData.bridges_[i] = int.Parse(bridges[i]);
                }
            }
        }
        //Huecos
        if (cabecera.Length > 5)
        {
            if (cabecera[5] != "") {
                string[] emptys = cabecera[5].Split(':');
                levelData.emptys_ = new int[emptys.Length];
                for (int i = 0; i < emptys.Length; i++)
                {
                    levelData.emptys_[i] = int.Parse(emptys[i]);
                }
            }
        }
        //Paredes
        if (cabecera.Length > 6)
        {
            if (cabecera[6] != "")
            {
                string[] walls = cabecera[6].Split(':');
                levelData.wall_ = new Vector2[walls.Length];
                for (int i = 0; i < walls.Length; i++)
                {
                    string[] wallaux = walls[i].Split('|');
                    levelData.wall_[i].x = int.Parse(wallaux[0]);
                    levelData.wall_[i].y = int.Parse(wallaux[1]);
                }
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

    public void UseHint()
    {
        if (GameManager.GetInstance().GetHints() > 0)
        {
            gridManager.UseHint();
            GameManager.GetInstance().DecreaseHint();
            SetHintText();
        }
    }

    public void NextLevel()
    {
        GameManager.GetInstance().NextLevel();
    }

    public void PrevLevel()
    {
        GameManager.GetInstance().PrevLevel();
    }

    public void ResetLevel()
    {
        GameManager.GetInstance().ToLevelScene();
    }

    public void BackToMenu()
    {
        GameManager.GetInstance().ToMenuScene();
    }

    public void SetLevel(TextAsset pack, int level, Color categoryColor)
    {
        levelData_ = CreateLevel(pack, level);
        gridManager.CreateLevel(levelData_);

        SetSteps(0);

        SetFlowsText();
        SetPipeText();
        SetMovesText();
        SetHintText();
        categoryColor_ = categoryColor;
        SetLevelText(level, levelData_.width, levelData_.height, categoryColor_);
        marker.SetSprite(GameManager.GetInstance().GetPassedLevelInfo());
    }

    public void Win()
    {
        finishPanel_.SetActive(true);
        finishPanel_.GetComponent<FinishPanel>().SetFinishPanel(GetSteps() == levelData_.numFlows, GetSteps());
        canPlay_ = false;
        GameManager.GetInstance().SaveLevel();
        marker.SetSprite(GameManager.GetInstance().GetPassedLevelInfo());
    }

    public void SetPlay(bool aux)
    {
        canPlay_ = aux;
    }

    public void SetPipeText()
    {
        float percentage = Mathf.Round(((float)count_ / (float)maxCount_)*100);
        pipeText.text = "tuber�a: " + percentage + "%";
    }

    public void SetFlowsText()
    {
        flowsText.text = "flujos: " + flowsCompleted_ + "/" + levelData_.numFlows;
    }

    public void SetMovesText()
    {
        movesText.text = "pasos: " + steps_ + " r�cord: " + GameManager.GetInstance().GetLevelMoves();
    }

    public void SetHintText()
    {
        hintText.text = GameManager.GetInstance().GetPlayerData()._hints + " x";
    }

    public void SetLevelText(int level, int width, int height, Color color)
    {
        levelText.text = "nivel " + level;
        levelText.color = color;
        sizeText.text = width + "x" + height;
    }

    public bool GetCanPlay()
    {
        return canPlay_;
    }

    #region Getters y setters
    public int GetMaxCount()
    {
        return maxCount_;
    }

    public int GetCount()
    {
        return count_;
    }

    public int GetSteps()
    {
        return steps_;
    }

    public int GetFlowsCompleted()
    {
        return flowsCompleted_;
    }

    public int GetHintNum()
    {
        return hintNums_;
    }
    public void SetMaxCount(int aux)
    {
        maxCount_ = aux;
    }

    public void SetCount(int aux)
    {
        count_ = aux;
    }

    public void SetSteps(int aux)
    {
        steps_ = aux;
    }

    public void SetFlowsCompleted(int aux)
    {
        flowsCompleted_ = aux;
    }

    public void SetHintNum(int aux)
    {
        hintNums_ = aux;
    }

    public void AddMaxCount(int aux)
    {
        maxCount_ += aux;
    }

    public void AddCount(int aux)
    {
        count_ += aux;
    }

    public void AddSteps(int aux)
    {
        steps_ += aux;
    }
    public void AddFlowsCompleted(int aux)
    {
        flowsCompleted_ += aux;
    }
    public void AddHintNum(int aux)
    {
        hintNums_ += aux;
    }

    public LevelData GetLevelData()
    {
        return levelData_;
    }

    #endregion

    LevelData levelData_;
    public GridManager gridManager;
    public GameObject finishPanel_;

    bool canPlay_ = true;

    Color categoryColor_;

    int maxCount_;
    int count_;
    int steps_;

    int flowsCompleted_;
    int hintNums_;

    // Textos de progresion de nivel
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI flowsText;
    public TextMeshProUGUI movesText;
    public TextMeshProUGUI pipeText;
    public TextMeshProUGUI hintText;
    public CompletedMarker marker;

}
