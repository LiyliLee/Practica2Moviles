using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontAdjustment : MonoBehaviour
{
    Text text_;
    RectTransform tr_;
    void Start()
    {
        text_ = GetComponent<Text>();
        int previous = text_.fontSize;
        int relacion = 1536 / previous;
        text_.fontSize = (Screen.width*previous)/1536;

        tr_ = GetComponent<RectTransform>();
        Rect rect = tr_.rect;
        int previousx = (int)rect.position.x;
        int previousy = (int)rect.position.y;
        int relacionx = 1536 / previousx;
        int relaciony = 2048 / previousy;
        tr_.rect.Set((Screen.width * previousx) / 1536, (Screen.height * previousy) / 2048, rect.width, rect.height);
    }
}
