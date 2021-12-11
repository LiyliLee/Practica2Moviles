using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public void setCell(bool isflow, bool isBridge, bool isEmpty, string color, int numCell)
    {
        isFlow_ = isflow;
        isBridge_ = isBridge;
        isEmpty_ = isEmpty;
        color_ = color;
        numCell_ = numCell;

        setPaths();
    }

    public void setWalls(bool up, bool down, bool right, bool left)
    {
        if (up) walls_[2].SetActive(true);
        if (down) walls_[3].SetActive(true);
        if (right) walls_[1].SetActive(true);
        if (left) walls_[0].SetActive(true);
    }

    public void setActivePath(int dir)
    {
        paths_[dir].GetComponent<SpriteRenderer>().color = prev_.GetComponent<SpriteRenderer>().color;
        paths_[dir].SetActive(true);
    }

    public void setPreviousCell(Cell c)
    {
        prev_ = c;
    }

    public Cell getPreviousCell()
    {
        return prev_;
    }

    private void setPaths()
    {
        int rot = 0;
        for(int i = 0; i < paths_.Length; i++)
        {
            paths_[i].transform.rotation = Quaternion.Euler(0, 0, rot);
                rot += 90;
        }
    }

    private bool isFlow_;
    private bool isEmpty_;
    private bool isBridge_;

    private int numCell_;

    public GameObject bridgeSprite_;

    //izq, der, arr, ab
    public GameObject[] walls_;

    //arr, ab, izq, der
    public GameObject[] paths_;

    //0 empty, 1 red, 2 blue, ...
    private string color_;

    private Cell prev_, next_;

    void Start()
    {
        transform.localScale = new Vector2(1, 1);

        if (isBridge_)
        {
            Instantiate(bridgeSprite_, transform);
        }
        
        if(isFlow_)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            //Transformar el string hexadecimal del color a RGB 
            string rs = color_[0].ToString() + color_[1].ToString();
            int r = System.Convert.ToInt32(rs, 16);
            string gs = color_[2].ToString() + color_[3].ToString();
            int g = System.Convert.ToInt32(gs, 16);
            string bs = color_[4].ToString() + color_[5].ToString();
            int b = System.Convert.ToInt32(bs, 16);
            GetComponent<SpriteRenderer>().color = new Color(r, g, b);
        }
    }

    public bool isFlow() { return isFlow_; }
    public bool isBridge() { return isBridge_; }
    public bool isEmpty() { return isEmpty_; }


}
