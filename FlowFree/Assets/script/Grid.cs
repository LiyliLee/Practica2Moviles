using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Grid : MonoBehaviour
{
    private void Awake()
    {
        //Para ajustar a la pantalla
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

        //Flows
        for (int i = 0; i < levelData_.numFlows; i++)
        {
            int cell1 = levelData_.solutions_[i][0];
            int cell2 = levelData_.solutions_[i][levelData_.solutions_[i].Count-1];
            board_[cell1 / width_, cell1 % height_] = Instantiate(cellPrefab_, new Vector3(-(width_ / 2) + (cell1 % width_), height_ / 2 - (cell1 / height_)), Quaternion.identity, transform).GetComponent<Cell>();
            board_[cell1 / width_, cell1 % height_].setCell(true, false, false, colors_[i], cell1);
            board_[cell1 / width_, cell1 % height_].setWalls(cell1 / width_ == 0, cell1 / width_ == width_ - 1, cell1 % height_ == height_ - 1, cell1 % height_ == 0);
            board_[cell2 / width_, cell2 % height_] = Instantiate(cellPrefab_, new Vector3(-(width_ / 2) + (cell2 % width_), height_ / 2 - (cell2 / height_)), Quaternion.identity, transform).GetComponent<Cell>();
            board_[cell2 / width_, cell2 % height_].setCell(true, false, false, colors_[i], cell2);
            board_[cell2 / width_, cell2 % height_].setWalls(cell2 / width_ == 0, cell2 / width_ == width_ - 1, cell2 % height_ == height_ - 1, cell2 % height_ == 0);
        }

        //Bridges
        if (levelData_.bridges_ != null)
        {
            for (int i = 0; i < levelData_.bridges_.Length; i++)
            {
                int cell1 = levelData_.bridges_[i];
                board_[cell1 / width_, cell1 % height_] = Instantiate(cellPrefab_, new Vector3(-(width_/2)+(cell1 % width_), height_/2-(cell1 / height_)), Quaternion.identity, transform).GetComponent<Cell>();
                board_[cell1 / width_, cell1 % height_].setCell(false, true, false, "000000", cell1);
                board_[cell1 / width_, cell1 % height_].setWalls(cell1 / width_ == 0, cell1 / width_ == width_ - 1, cell1 % height_ == height_ - 1, cell1 % height_ == 0);
            }
        }

        //Emptys
        if (levelData_.emptys_ != null)
        {
            for (int i = 0; i < levelData_.emptys_.Length; i++)
            {
                int cell1 = levelData_.emptys_[i];
                board_[cell1 / width_, cell1 % height_] = Instantiate(cellPrefab_, new Vector3(-(width_ / 2) + (cell1 % width_), height_ / 2 - (cell1 / height_)), Quaternion.identity, transform).GetComponent<Cell>();
                board_[cell1 / width_, cell1 % height_].setCell(false, false, true, "000000", cell1);
                board_[cell1 / width_, cell1 % height_].setWalls(cell1 / width_ == 0, cell1 / width_ == width_ - 1, cell1 % height_ == height_ - 1, cell1 % height_ == 0);
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

        //Rest
        for (int i = 0; i < width_ * height_; i++)
        {
            if (board_[i / width_, i % height_] == null)
            {
                board_[i / width_, i % height_] = Instantiate(cellPrefab_, new Vector3(-(width_ / 2) + (i % width_), height_ / 2 - (i / height_)), Quaternion.identity, transform).GetComponent<Cell>();
                board_[i / width_, i % height_].setCell(false, false, false, "000000", i);
                board_[i / width_, i % height_].setWalls(i / width_ == 0, i / width_ == width_ - 1, i % height_ == height_ - 1, i % height_ == 0);
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
