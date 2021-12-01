using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Cell(bool isflow, bool isBridge, bool isEmpty, int color)
    {
        isFlow_ = isflow;
        isBridge_ = isBridge;
        isEmpty_ = isEmpty;
        color_ = color;
    }

    private bool isFlow_;
    private bool isEmpty_;
    private bool isBridge_;

    public GameObject bridgeSprite_;

    //izq, der, arr, ab
    public GameObject[] walls_ = new GameObject[4];

    //arr, ab, izq, der
    public GameObject[] paths_ = new GameObject[4];

    //0 empty, 1 red, 2 blue, ...
    private int color_;

    void Start()
    {
        transform.localScale = new Vector2(1, 1);

        if (isBridge_)
        {
            Instantiate(bridgeSprite_, transform);
        }
        
    }

    public bool isFlow() { return isFlow_; }
    public bool isBridge() { return isBridge_; }
    public bool isEmpty() { return isEmpty_; }


}
