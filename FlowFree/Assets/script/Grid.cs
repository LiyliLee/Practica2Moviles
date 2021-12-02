using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Grid : MonoBehaviour
{
    private void Awake()
    {
        RectTransform myRect = GetComponent<RectTransform>();

        Vector2 rectMiddle = new Vector2(0.5f, 0.5f);

        float horizontalSize = 0.5f;
        float verticalSize = 0.75f;

        myRect.sizeDelta = Vector2.zero;
        myRect.anchoredPosition = Vector2.zero;

        myRect.anchorMin = new Vector2(rectMiddle.x - horizontalSize / 2, rectMiddle.y - verticalSize / 2);
        myRect.anchorMax = new Vector2(rectMiddle.x + horizontalSize / 2, rectMiddle.y + verticalSize / 2);
    }
    public void createLevel(TextAsset levelPack, int level)
    {
        levelNum_ = level;
        levelData_ = levelManager_.CreateLevel(levelPack, level);

        board_ = new Cell[levelData_.width, levelData_.height];

        width_ = levelData_.width;
        height_ = levelData_.height;
        //Rest
        for(int i = 0; i < width_*height_; i++)
        {
            board_[i / width_, i % height_] = Instantiate(cellPrefab_, transform).GetComponent<Cell>();
            board_[i / width_, i % height_].setCell(false, false, false, "000000");
        }

        //Flows
        for (int i = 0; i < levelData_.numFlows; i++)
        {
            int cell1 = levelData_.solutions_[i][0];
            int cell2 = levelData_.solutions_[i][levelData_.solutions_[0].Count-1];
            board_[cell1 / width_, cell1 % height_] = Instantiate(cellPrefab_, transform).GetComponent<Cell>();
            board_[cell1 / width_, cell1 % height_].setCell(true, false, false, colors_[i]);
            board_[cell2 / width_, cell2 % height_] = Instantiate(cellPrefab_, transform).GetComponent<Cell>();
            board_[cell2 / width_, cell2 % height_].setCell(true, false, false, colors_[i]);
        }

        //Bridges
        if (levelData_.bridges_ != null)
        {
            for (int i = 0; i < levelData_.bridges_.Length; i++)
            {
                int cell1 = levelData_.bridges_[i];
                board_[i / width_, i % height_] = Instantiate(cellPrefab_, transform).GetComponent<Cell>();
                board_[i / width_, i % height_].setCell(false, true, false, "000000");
            }
        }

        //Emptys
        if (levelData_.emptys_ != null)
        {
            for (int i = 0; i < levelData_.emptys_.Length; i++)
            {
                int cell1 = levelData_.emptys_[i];
                board_[i / width_, i % height_] = Instantiate(cellPrefab_, transform).GetComponent<Cell>();
                board_[i / width_, i % height_].setCell(false, false, true, "000000");
            }
        }

        if (levelData_.wall_ != null)
        {
            //Walls
            for (int i = 0; i < levelData_.wall_.Length; i++)
            {
                //
            }
        }
    }

    int width_, height_;
    int levelNum_;
    private Cell[,] board_;
    public LevelManager levelManager_;
    private LevelManager.LevelData levelData_;

    public GameObject cellPrefab_;

    private string[] colors_ = {"FF0000", "008D00", "0C29FE", "EAE000",
        "FB8900", "00FFFF", "FF0AC9", "A52A2A", "800080", "FFFFFF", "9F9FBD", "00FF00", "A18A51", "09199F",
        "008080", "FE7CEC"};
}
