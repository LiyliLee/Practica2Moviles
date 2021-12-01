using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Grid : MonoBehaviour
{
    void Start()
    {
        levelManager_ = new LevelManager();
    }

    public void createLevel(TextAsset levelPack, int level)
    {
        levelData_ = levelManager_.CreateLevel(levelPack, level);

        board_ = new Cell[levelData_.width, levelData_.height];

        //Flows
        for(int i = 0; i < levelData_.numFlows; i++)
        {
            int cell1 = levelData_.solutions_[0][0];
            int cell2 = levelData_.solutions_[0][levelData_.solutions_[0].Count-1];
            board_[cell1 % width_, width_ - cell1] = new Cell(true, false, false, colors_[i]);
            board_[cell2 % width_, width_ - cell2] = new Cell(true, false, false, colors_[i]);
        }

        //Bridges
        for (int i = 0; i < levelData_.bridges_.Length; i++)
        {
            int cell1 = levelData_.bridges_[i];
            board_[cell1 % width_, width_ - cell1] = new Cell(false, true, false, colors_[i]);
        }

        //Emptys
        for (int i = 0; i < levelData_.emptys_.Length; i++)
        {
            int cell1 = levelData_.emptys_[i];
            board_[cell1 % width_, width_ - cell1] = new Cell(false, false, true, colors_[i]);
        }

        //Walls
        for (int i = 0; i < levelData_.wall_.Length; i++)
        {
            //
        }

        //Rest
        for (int i = 0; i < levelData_.wall_.Length; i++)
        {
            //
        }
    }

    int width_, height_;
    int levelNum_;
    private Cell[,] board_;
    private LevelManager levelManager_;
    private LevelManager.LevelData levelData_;

    private int[] colors_ = {0xFF0000, 0x008D00, 0x0C29FE, 0xEAE000,
        0xFB8900, 0x00FFFF, 0xFF0AC9, 0xA52A2A, 0x800080, 0xFFFFFF, 0x9F9FBD, 0x00FF00, 0xA18A51, 0x09199F,
        0x008080, 0xFE7CEC};
}
