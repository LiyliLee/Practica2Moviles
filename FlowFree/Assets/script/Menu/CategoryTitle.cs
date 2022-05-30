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


    public void Init(Color c, string name, LevelSelect ls)
    {
        // Inicializa el nombre de cada categoria
        categoryName.text = name;
        background.color = new Color(c.r,c.g,c.b ,0.5f);
        line.color = c;
        _levelSelectPanel = ls;
    }

    public void SetPacks(CategoryLevel category, VerticalLayoutGroup verticalLayout, Dictionary<string, PlayerData.PassedLevelInfo[]> passedInfo)
    {
        // inicializa los packs dentro de cada categoria
        PackSelect pack;

        for(int i = 0; i < category.packs.Length; i++)
        {
            pack = Instantiate(_packSelectPrefab, verticalLayout.transform);
            pack.SetPack(category.categoryName, category.packs[i].name, category.color, passedInfo[category.packs[i].name]);
            pack.SetCallback(_levelSelectPanel);
        }
    }
}
