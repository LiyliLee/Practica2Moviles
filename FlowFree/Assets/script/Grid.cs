using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        board_ = new Cell[5, 5];

        board_[0, 0] = new Cell(true, false, false, Color.red);
        board_[4, 1] = new Cell(true, false, false, Color.red);

        board_[0, 2] = new Cell(true, false, false, Color.blue);
        board_[3, 1] = new Cell(true, false, false, Color.blue);

        board_[0, 4] = new Cell(true, false, false, Color.green);
        board_[3, 3] = new Cell(true, false, false, Color.green);

        board_[1, 2] = new Cell(true, false, false, Color.magenta);
        board_[4, 2] = new Cell(true, false, false, Color.magenta);

        board_[1, 4] = new Cell(true, false, false, Color.yellow);
        board_[4, 3] = new Cell(true, false, false, Color.yellow);

        for(int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if(!board_[i, j].isFlow())
                {
                    board_[i, j] = new Cell(false, false, false, Color.red);
                }
            }
        }
    }

    public void createMap(int width, int height, int level, int numFlows)
    {
        width_ = width;
        height_ = height;
        levelNum_ = level;

        for(int i = 0; i < numFlows; i++)
        {
            
        }
    }

    int width_, height_;
    int levelNum_;
    private Cell[,] board_;
}
