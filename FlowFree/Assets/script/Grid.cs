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

        cursorObject_.SetActive(false);
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
            board_[cell1 % width_, cell1 / width_] = Instantiate(cellPrefab_, new Vector3(-(width_ / 2) + (cell1 % width_), height_ / 2 - (cell1 / height_)), Quaternion.identity, transform).GetComponent<Cell>();
            board_[cell1 % width_, cell1 / width_].setCell(true, false, false, colors_[i], cell1);
            board_[cell1 % width_, cell1 / width_].setWalls(cell1 / width_ == 0, cell1 / width_ == width_ - 1, cell1 % height_ == height_ - 1, cell1 % height_ == 0);
            board_[cell2 % width_, cell2 / width_] = Instantiate(cellPrefab_, new Vector3(-(width_ / 2) + (cell2 % width_), height_ / 2 - (cell2 / height_)), Quaternion.identity, transform).GetComponent<Cell>();
            board_[cell2 % width_, cell2 / width_].setCell(true, false, false, colors_[i], cell2);
            board_[cell2 % width_, cell2 / width_].setWalls(cell2 / width_ == 0, cell2 / width_ == width_ - 1, cell2 % height_ == height_ - 1, cell2 % height_ == 0);
        }

        //Bridges
        if (levelData_.bridges_ != null)
        {
            for (int i = 0; i < levelData_.bridges_.Length; i++)
            {
                int cell1 = levelData_.bridges_[i];
                board_[cell1 % width_, cell1 / width_] = Instantiate(cellPrefab_, new Vector3(-(width_/2)+(cell1 % width_), height_/2-(cell1 / height_)), Quaternion.identity, transform).GetComponent<Cell>();
                board_[cell1 % width_, cell1 / width_].setCell(false, true, false, "000000", cell1);
                board_[cell1 % width_, cell1 / width_].setWalls(cell1 / width_ == 0, cell1 / width_ == width_ - 1, cell1 % height_ == height_ - 1, cell1 % height_ == 0);
            }
        }

        //Emptys
        if (levelData_.emptys_ != null)
        {
            for (int i = 0; i < levelData_.emptys_.Length; i++)
            {
                int cell1 = levelData_.emptys_[i];
                board_[cell1 % width_, cell1 / width_] = Instantiate(cellPrefab_, new Vector3(-(width_ / 2) + (cell1 % width_), height_ / 2 - (cell1 / height_)), Quaternion.identity, transform).GetComponent<Cell>();
                board_[cell1 % width_, cell1 / width_].setCell(false, false, true, "000000", cell1);
                board_[cell1 % width_, cell1 / width_].setWalls(cell1 / width_ == 0, cell1 / width_ == width_ - 1, cell1 % height_ == height_ - 1, cell1 % height_ == 0);
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
            if (board_[i % width_, i / width_] == null)
            {
                board_[i % width_, i / width_] = Instantiate(cellPrefab_, new Vector3(-(width_ / 2) + (i % width_), height_ / 2 - (i / height_)), Quaternion.identity, transform).GetComponent<Cell>();
                board_[i % width_, i / width_].setCell(false, false, false, "000000", i);
                board_[i % width_, i / width_].setWalls(i / width_ == 0, i / width_ == width_ - 1, i % height_ == height_ - 1, i % height_ == 0);
            }
        }
    }


    public void ProcessInput(InputManager.MoveType move, Vector2 pos)
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

        if (move == InputManager.MoveType.DRAG)
        {
            foreach(Cell c in board_)
            {
                board_[i / width_, i % height_] = Instantiate(cellPrefab_, new Vector3(-(width_ / 2) + (i % width_), height_ / 2 - (i / height_)), Quaternion.identity, transform).GetComponent<Cell>();
                board_[i / width_, i % height_].setCell(false, false, false, "000000", i);
                board_[i / width_, i % height_].setWalls(i / width_ == 0, i / width_ == width_ - 1, i % height_ == height_ - 1, i % height_ == 0);
                if (c == hit.transform.gameObject.GetComponent<Cell>())
                {
                    if (c.isFlow())
                    {
                        Color color = c.gameObject.GetComponent<SpriteRenderer>().color;
                        color = new Color(color.r, color.g, color.b, 0.3f);

                        cursorObject_.SetActive(true);
                        cursorObject_.GetComponent<SpriteRenderer>().color = color;

                        lastTouchedCell_ = c;

                    }
                    else
                    {
                        c.setPreviousCell(lastTouchedCell_);
                        // der
                        if (c.getPreviousCell().transform.position.x < c.transform.position.x && c.getPreviousCell().transform.position.y == c.transform.position.y)
                            c.setActivePath(1);
                        // izq
                        else if (c.getPreviousCell().transform.position.x > c.transform.position.x && c.getPreviousCell().transform.position.y == c.transform.position.y)
                            c.setActivePath(3);
                        // down
                        else if (c.getPreviousCell().transform.position.y > c.transform.position.y && c.getPreviousCell().transform.position.x == c.transform.position.x)
                            c.setActivePath(0);
                        // up
                        else if (c.getPreviousCell().transform.position.y < c.transform.position.y && c.getPreviousCell().transform.position.x == c.transform.position.x)
                            c.setActivePath(2);

                    }

                    cursorObject_.transform.position = Camera.main.ScreenToWorldPoint(pos);
                    cursorObject_.transform.position = new Vector3(cursorObject_.transform.position.x, cursorObject_.transform.position.y, 0);
                }
            }
        }
        else
        {
            cursorObject_.SetActive(false);
            lastTouchedCell_ = null;
        }
    }

    int width_, height_;
    int levelNum_;
    private Cell[,] board_;
    private Cell lastTouchedCell_ = null;
    public LevelManager levelManager_;
    private LevelManager.LevelData levelData_;

    // para saber las casillas que llevan tuberia
    private bool [,] flowed_;

    public GameObject cellPrefab_;
    public GameObject cursorObject_;

    private string[] colors_ = {"FF0000", "008D00", "0C29FE", "EAE000",
        "FB8900", "00FFFF", "FF0AC9", "A52A2A", "800080", "FFFFFF", "9F9FBD", "00FF00", "A18A51", "09199F",
        "008080", "FE7CEC"};
}
