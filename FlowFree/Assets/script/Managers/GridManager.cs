using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public void CreateLevel(LevelManager.LevelData level)
    {
        levelData_ = level;

        width_ = levelData_.width;
        height_ = levelData_.height;

        board_ = new Cell[width_, height_];
        currents_ = new List<List<Cell>>();

        //En vez de adaptar el casillero al tamaño, adaptamos la camara al tamaño del tablero
        cam.orthographicSize = (height_ / 2) * 1.9f;
        if (cam.aspect * (cam.orthographicSize) < (float)width_ / 2)
            cam.orthographicSize = (width_ / (cam.aspect * 2));

        maxCount_ = 0;
        count_ = 0;

        //Todas las casillas
        for(int i = 0; i < width_*height_; i++)
        {
            float realPosX, realPosY;
            float offset;
            if (width_ % 2 == 1) offset = width_ / 2;
            else offset = width_ / 2 - 0.5f;
            realPosX = i%width_ - offset;
            realPosY = offset - i/width_;
            board_[i % width_, i / width_] = Instantiate(cellPrefab_, new Vector3(realPosX, realPosY, 0), Quaternion.identity, transform).GetComponent<Cell>();
            board_[i % width_, i / width_].setCell(false, false, "000000", i);
            board_[i % width_, i / width_].setWalls(i / width_ == 0, i / width_ == width_ - 1, i % height_ == height_ - 1, i % height_ == 0);
            maxCount_++;
        }

        //Flows
        for(int i = 0; i < level.numFlows; i++)
        {
            int cell1 = levelData_.solutions_[i][0];
            int cell2 = levelData_.solutions_[i][levelData_.solutions_[i].Count - 1];
            board_[cell1 % width_, cell1 / width_].setCell(true, false, colors_[i], cell1);
            board_[cell2 % width_, cell2 / width_].setCell(true, false, colors_[i], cell1);
            currents_.Add(new List<Cell>());
            maxCount_--;
            maxCount_--;
        }

        //Vacios
        if (levelData_.emptys_ != null)
        {
            for (int i = 0; i < levelData_.emptys_.Length; i++)
            {
                int cell1 = levelData_.emptys_[i];
                board_[cell1 % width_, cell1 / width_].setCell(false, true, "000000", cell1);
                board_[cell1 % width_, cell1 / width_].setWalls(cell1 / width_ == 0, cell1 / width_ == width_ - 1, cell1 % height_ == height_ - 1, cell1 % height_ == 0);
            }
        }

        //Paredes
        if (levelData_.wall_ != null)
        {
            for (int i = 0; i < levelData_.wall_.Length; i++)
            {
                int cell1num = (int)levelData_.wall_[i].x;
                int cell2num = (int)levelData_.wall_[i].y;
                Cell cell1 = board_[cell1num % width_, cell1num / width_];
                Cell cell2 = board_[cell2num % width_, cell2num / width_];
                if (cell1.transform.position.x > cell2.transform.position.x)
                {
                    cell1.setAWall(2, true);
                    cell2.setAWall(3, true);
                } 
                else if(cell1.transform.position.x < cell2.transform.position.x)
                {
                    cell1.setAWall(3, true);
                    cell2.setAWall(2, true);
                } 
                else if(cell1.transform.position.y > cell2.transform.position.y)
                {
                    cell1.setAWall(1, true);
                    cell2.setAWall(0, true);
                } 
                else 
                {
                    cell1.setAWall(0, true);
                    cell2.setAWall(1, true);
                }
            }
        }

        levelManager_.SetPlay(true);
    }

    public void setPlay(bool aux)
    {
        levelManager_.SetPlay(aux);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && levelManager_.GetCanPlay())
        {
            Touch touch = Input.GetTouch(0);
            float x = cam.ScreenToWorldPoint(touch.position).x;
            float y = cam.ScreenToWorldPoint(touch.position).y;

            float offsetX = (float)(width_ % 2 == 1 ? (int)(width_ / 2) : (int)(width_ / 2) - 0.5);
            float offsetY = (float)(height_ % 2 == 1 ? (int)(height_ / 2) : (int)(height_ / 2) - 0.5);
            Vector3 logicPos = new Vector2((float)Math.Round(x + offsetX), (float)Math.Round(offsetY - y));

            //Posicion logica de la casilla escogida
            int logicX = (int)logicPos.x;
            int logicY = (int)logicPos.y;

            // Si esta fuera del tablero
            if (logicX < 0 || logicX >= width_ || logicY < 0 || logicY >= height_)
                return;

            Cell clicked = board_[logicX, logicY];

            if (lastFlowClicked_ == null && touch.phase == TouchPhase.Began)
            {
                // Si no es flow
                if (!clicked.isFlow() && clicked.getActive())
                {
                    lastCurrent_ = GetCurrent(clicked);
                    lastFlowClicked_ = lastCurrent_.First();
                    if (lastFlowClicked_.isFlow()) lastFlowClicked_ = lastCurrent_.First();
                }
                // Si es un flow
                else if (clicked.isFlow())
                {
                    ClearCurrent(GetCurrent(clicked), 0);
                    lastFlowClicked_ = clicked;
                    lastCurrent_ = GetCurrent(lastFlowClicked_);
                    lastCurrent_.Add(clicked);
                }
                else
                    return;

                //Comprobamos si ha habido cambio de color entre los inputs para añadir pasos
                bool aux = (pastCurrents_ == null) || (lastColorClicked_ != lastFlowClicked_.getColor());
                if (aux)
                {
                    steps_++;
                    levelManager_.SetMovesText(steps_, getNumFlows());
                }
                if (aux || lastColorClicked_ == lastFlowClicked_.getColor())
                {
                    pastCurrents_ = new List<List<Cell>>();
                    for(int i = 0; i < currents_.Count;i++)
                    {
                        pastCurrents_.Add(new List<Cell>(currents_[i]));
                    }
                    lastColorClicked_ = lastFlowClicked_.getColor();
                }
            }
            else if (lastFlowClicked_)
            {
                //Intentamos hacer conexion entre la ultima casilla tocada y la actual
                Advance(clicked);

                //Levantamos el dedo
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    //Reseteamos los marcadores de ultimo flow y ultima corriente pulsada
                    if (lastCurrent_.Count == 1) lastFlowClicked_.DisconnectCell();
                    lastFlowClicked_ = null;
                    lastCurrent_ = null;
                }
            }
        }
    }
    
    private void Advance(Cell clicked)
    {
        //Si es flow de otro color
        if (clicked.isFlow() && clicked.getColor() != lastFlowClicked_.getColor())
        {
            lastFlowClicked_ = null;
            lastCurrent_ = null;
            return;
        }

        //Si estamos volviendo a la misma corriente
        if (clicked.getColor() == lastFlowClicked_.getColor())
        {
            if (lastCurrent_.Count > 0 && ((clicked != lastCurrent_.Last() || lastCurrent_.First() == lastCurrent_.Last()) || (clicked.isFlow() && clicked == lastFlowClicked_)))
            {
                int index = lastCurrent_.FindIndex(cell => cell == clicked);

                //Desconectamos las casillas de la corriente desde la que hemos pulsado hacia delante
                List<Cell> eraseCells = lastCurrent_.GetRange(index + 1, lastCurrent_.Count - (index + 1));

                if (index >= 0)
                    ClearCurrent(lastCurrent_, index + 1);

                foreach (Cell cell in eraseCells)
                {
                    RecoverCurrent(cell);
                }
            }
        }

        List<Cell> previousCurrent = GetCurrent(clicked);
        if (previousCurrent != null && !previousCurrent.Contains(clicked))
            previousCurrent = null;

        AddCellToCurrent(clicked, ref lastCurrent_);
        
        //Comprobamos si todas las casillas y las corrientes estan completas
        CheckWin();
    }

    void CheckWin()
    {
        int i = 0;
        bool aux = true;
        //Si en todas las corrientes la primera y la ultima casilla son flows
        while(i < currents_.Count && aux)
        {
            if (currents_[i].Count > 2)
            {
                if (currents_[i].First().isFlow() && currents_[i].Last().isFlow())
                    aux = true;
                else aux = false;
            }
            else aux = false;
            i++;
        }
        if (aux)
        {
            levelManager_.Win();
        }
    }

    void RecoverCurrent(Cell cell)
    {
        int i = 0;
        bool aux = false;
        //Comprobamos si la casilla pertenece a otra corriente
        while(i < pastCurrents_.Count && !aux)
        {
            int j = 0;
            while(j < pastCurrents_[i].Count && !aux)
            {
                if (cell.getNum() == pastCurrents_[i][j].getNum())
                    aux = true;
                j++;
            }
            i++;
        }

        //Si es asi, 
        if(aux)
        {
            List<Cell> curr1 = currents_[i - 1];
            List<Cell> curr2 = pastCurrents_[i - 1];
            for(int t = 0; t < curr2.Count; t++)
            {
                int cellNum = curr2[t].getNum();
                if (board_[cellNum % width_, cellNum / width_].getColor() == lastFlowClicked_.getColor())
                    return;

                if(!curr1.Contains(board_[cellNum%width_, cellNum/width_]))
                {
                    board_[cellNum % width_, cellNum / width_].setColor(colors_[i - 1]);
                    ConnectCells(board_[cellNum % width_, cellNum / width_], curr1.Last());
                    curr1.Add(board_[cellNum % width_, cellNum / width_]);
                }
            }
        }
    }

    private void AddCellToCurrent(Cell clicked, ref List<Cell> current)
    {
        //Si no lo tiene
        if (!current.Contains(clicked))
        {
            //Si no es un flow o es un flow del mismo color
            if ((!clicked.isFlow() && !clicked.getActive()) || (clicked.isFlow() && clicked.getColor() == lastFlowClicked_.getColor() && clicked != lastCurrent_.First()))
            {
                //Si no es un flow o el ultimo añadido es el final de la corriente
                if (!current.Last().isFlow() || (current.Last().isFlow() && current.Last() == current.First()))
                {
                    clicked.setColor(current.Last().getColor());
                    ConnectCells(clicked, current.Last());
                    current.Add(clicked);
                    clicked.setActive(true);
                    //Si conectamos con un flow, no contamos el paso, ya que su casilla ya estaba rellena
                    if (!clicked.isFlow())
                        count_++;
                    levelManager_.SetPipeText(count_, maxCount_);
                }
            }
        }
    }

    void ConnectCells(Cell from, Cell to)
    {
        Vector3 dir = to.transform.position - from.transform.position;

        from.setPathColor(from.getColor());

        //Activamos los caminos dependiendo de la direccion en la que queremos conectar
        if (dir.x != 0)
        {
            if (dir.x == -1)
            {
                from.setActivePath(2);
                to.setActivePath(3);
            }
            else if (dir.x == 1)
            {
                from.setActivePath(3);
                to.setActivePath(2);
            }
        }
        else
        {
            if (dir.y == -1)
            {
                from.setActivePath(1);
                to.setActivePath(0);
            }
            else if (dir.y == 1)
            {
                from.setActivePath(0);
                to.setActivePath(1);
            }
        }

        to.setPathColor(from.getColor());
    }

    private List<Cell> GetCurrent(Cell cell)
    {
        //Obtenemos la corriente buscada a traves del color de la casilla
        //(Las casillas de la corriente 1 tendran el valor 1 del array de colores)
        int index = -1;
        for (int i = 0; i < colors_.Length; i++)
        {
            if (colors_[i] == cell.getColor()) index = i;
        }
        if (index == -1) return null;
        else return currents_[index];
    }

    private void ClearCurrent(List<Cell> current, int index)
    {
        //Vamos desconectando casillas de la corriente desde el indice indicado
        for (int i = index; i < current.Count; i++)
        {
            current[i].DisconnectCell();
            if (!current[i].isFlow())
            {
                count_--;
                levelManager_.SetPipeText(count_, maxCount_);
            }
        }

        //Limpiamos la corriente desde el indice indicado siempre y cuando tenga más de 1 casilla (el flow)
        Cell firstCutCell = null;
        if (current.Count > 1) firstCutCell = current[index];
        current.RemoveRange(index, current.Count - index);

        if (current.Count == 1)
        {
            current[0].DisconnectCell();
        }
        else if (current.Count > 1)
        {
            Cell lastCell = current.Last();

            int size = board_.GetLength(0);
            float offsetX = (float)(size % 2 == 1 ? (int)(size / 2) : (int)(size / 2) - 0.5);
            size = board_.GetLength(1);
            float offsetY = (float)(size % 2 == 1 ? (int)(size / 2) : (int)(size / 2) - 0.5);
            Vector2 logicPos = new Vector2((float)Math.Round(lastCell.transform.position.x + offsetX), (float)Math.Round(offsetY - lastCell.transform.position.y));

            if (logicPos.y - 1 >= 0 && board_[(int)logicPos.x, (int)logicPos.y - 1] == firstCutCell)
                lastCell.ErasePath(0);
            else if (logicPos.y + 1 < height_ && board_[(int)logicPos.x, (int)logicPos.y + 1] == firstCutCell)
                lastCell.ErasePath(1);
            else if (logicPos.x - 1 >= 0 && board_[(int)logicPos.x - 1, (int)logicPos.y] == firstCutCell)
                lastCell.ErasePath(2);
            else lastCell.ErasePath(3);

        }
    }

    public int getSteps()
    {
        return steps_;
    }

    public int getNumFlows()
    {
        return levelData_.numFlows;
    }

    int width_, height_;
    private Cell[,] board_;
    public LevelManager levelManager_;
    private LevelManager.LevelData levelData_;

    private Cell lastFlowClicked_ = null;
    private List<Cell> lastCurrent_;
    private string lastColorClicked_;

    private List<List<Cell>> currents_;
    private List<List<Cell>> pastCurrents_;

    public GameObject cellPrefab_;
    public GameObject cursorObject_;

    public Camera cam;

    int maxCount_;
    int count_;
    int steps_;

    private string[] colors_ = {"FF0000", "008D00", "0C29FE", "EAE000",
        "FB8900", "00FFFF", "FF0AC9", "A52A2A", "800080", "FFFFFF", "9F9FBD", "00FF00", "A18A51", "09199F",
        "008080", "FE7CEC"};

}
