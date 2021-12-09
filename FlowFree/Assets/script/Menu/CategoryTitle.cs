using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryTitle : MonoBehaviour
{
    public Image back;
    public Image line;
    public Text CatName;

    public void Init(Color c, string name)
    {
        CatName.text = name;
        back.color = new Color(c.r,c.g,c.b ,0.5f);
        line.color= c;
    }
}
