using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Text levelNum;
    int num;
    int categoryId;
    int packId;
    public Image image;
    bool locked;

    public void Init(int nu, int catid, int unlock, int packid, Color col)
    {
        num = nu;
        levelNum.text = (num).ToString();
        levelNum.color = col;
        categoryId = catid;
        packId = packid;

    }
    public void setImage(Sprite img)
    {
        image.sprite = img;
    }

    public void selectLevel()
    {
        if (!locked)
        {
           // GameManager._instance.selecLevel(categoryId, packId, num);
        }
    }
}