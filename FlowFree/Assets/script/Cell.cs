using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector2(1, 1);
    }
    public void setCell(bool isflow, bool isEmpty, string color, int numCell)
    {
        isFlow_ = isflow;
        isEmpty_ = isEmpty;
        color_ = color;
        numCell_ = numCell;

        if (isFlow_)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            //Transformar el string hexadecimal del color a RGB 
            GetComponent<SpriteRenderer>().color = getColorRGB(color_);
            isActive_ = true;
        }
    }

    private Color getColorRGB(string color)
    {
        string rs = color_[0].ToString() + color_[1].ToString();
        int r = System.Convert.ToInt32(rs, 16);
        string gs = color_[2].ToString() + color_[3].ToString();
        int g = System.Convert.ToInt32(gs, 16);
        string bs = color_[4].ToString() + color_[5].ToString();
        int b = System.Convert.ToInt32(bs, 16);
        return new Color(r, g, b);
    }

    public void setWalls(bool up, bool down, bool right, bool left)
    {
        if (up) walls_[0].SetActive(true);
        if (down) walls_[1].SetActive(true);
        if (left) walls_[2].SetActive(true);
        if (right) walls_[3].SetActive(true);
    }

    public void setAWall(int wall, bool active)
    {
        //0 up 1 down 2 left 3 right
        walls_[wall].SetActive(active);
    }

    public void setActivePath(int dir)
    {
        //paths_[dir].GetComponent<SpriteRenderer>().color = prev_.GetComponent<SpriteRenderer>().color;
        paths_[dir].SetActive(true);
    }

    public void setPreviousCell(Cell c)
    {
        prev_ = c;
    }

    public void setColor(string color)
    {
        color_ = color;
    }
    public void setPathColor(string color)
    {
        for (int i = 0; i < 4; i++)
        {
            paths_[i].GetComponent<SpriteRenderer>().color = getColorRGB(color);
        }
        color_ = color;
    }

    public Cell getPreviousCell()
    {
        return prev_;
    }

    public void setActive(bool aux)
    {
        isActive_ = aux;
    }

    public bool getActive()
    {
        return isActive_;
    }

    public string getColor()
    {
        return color_;
    }

    public int getNum() {
        return numCell_;
    }

    public void DisconnectCell()
    {
        for(int i = 0; i < 4; i++)
        {
            paths_[i].SetActive(false);
        }

        isActive_ = false;
    }

    public void ErasePath(int index)
    {
        paths_[index].SetActive(false);
    }

    private bool isFlow_;
    private bool isEmpty_;
    private bool isActive_;

    private int numCell_;

    //izq, der, arr, ab
    public GameObject[] walls_;

    //arr, ab, izq, der
    public GameObject[] paths_;

    //0 empty, 1 red, 2 blue, ...
    private string color_;

    private Cell prev_, next_;

    

    public bool isFlow() { return isFlow_; }
    public bool isEmpty() { return isEmpty_; }


}
