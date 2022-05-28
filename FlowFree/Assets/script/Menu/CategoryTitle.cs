using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CategoryTitle : MonoBehaviour
{
    public Image background;
    public Image line;
    public TextMeshProUGUI categoryName;

    public PackSelect _packSelectPrefab = null;

    private LevelSelect _levelSelectPanel;


    public void Init(Color c, string name)
    {
        categoryName.text = name;
        background.color = new Color(c.r,c.g,c.b ,0.5f);
        line.color = c;
    }

    public void SetLevelPanel(LevelSelect ls)
    {
        _levelSelectPanel = ls;
    }

    public void SetPacks(CategoryLevel category, VerticalLayoutGroup verticalLayout)
    {
        PackSelect pack;

        for(int i = 0; i < category.packs.Length; i++)
        {
            Debug.Log("Setting up LevelPacket Button");
            pack = Instantiate(_packSelectPrefab, verticalLayout.transform);
            pack.SetPack(category.categoryName, category.packs[i].name, category.color);
            pack.SetCallback(_levelSelectPanel);
        }
    }
}
