using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    void Start()
    {
        //transform.localScale = new Vector2(1, 1);
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
            Color newcolor = getColorRGB(color_);
            GetComponent<SpriteRenderer>().color = newcolor;
            isActive_ = true;
        }
    }

    //Transforma un color en formato hexadecimal "XXXXXX" a valores RGB
    private Color getColorRGB(string color)
    {
        string rs = color_[0].ToString() + color_[1].ToString();
        float r = (float)System.Convert.ToInt32(rs, 16)/255.0f;
        string gs = color_[2].ToString() + color_[3].ToString();
        float g = (float)System.Convert.ToInt32(gs, 16)/255.0f;
        string bs = color_[4].ToString() + color_[5].ToString();
        float b = (float)System.Convert.ToInt32(bs, 16)/255.0f;
        return new Color(r, g, b);
    }

    // 0 arriba 1 abajo 2 izq 3 der
    public bool getActiveWall(int num)
    {
        return walls_[num].active;
    }

    public void setWalls(bool up, bool down, bool left, bool right)
    {
        if (up) walls_[0].SetActive(true);
        if (down) walls_[1].SetActive(true);
        if (left) walls_[2].SetActive(true);
        if (right) walls_[3].SetActive(true);
    }

    public void setAWall(int wall, bool active)
    {
        //0 arriba 1 abajo 2 izq 3 der
        walls_[wall].SetActive(active);
    }

    public void setActivePath(int dir)
    {
        paths_[dir].SetActive(true);
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

        if(!isFlow_)
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

    public GameObject[] walls_;

    public GameObject[] paths_;

    private string color_;
    

    public bool isFlow() { return isFlow_; }
    public bool isEmpty() { return isEmpty_; }


}
