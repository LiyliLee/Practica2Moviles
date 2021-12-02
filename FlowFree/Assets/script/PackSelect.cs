using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackSelect : MonoBehaviour
{
    // Start is called before the first frame update

    CategotyLevel[] categories;
    public PackButton packButtonPrefab;
    PackButton[][] packButtons;
    public GameObject scrollLevel;
    public GameObject levelContent;
    int packInCategories;
    public void Init()
    {
        categories = GameManager._instance.GetCategoties();
        packButtons = new PackButton[categories.Length][];
        for (int i = 0; i < categories.Length; i++)
        {
            packInCategories = categories[i].packs.Length;
            packButtons[i] = new PackButton[packInCategories];
            for (int j=0; i< packInCategories; j++)
            {
                packButtons[i][j] = Instantiate(packButtonPrefab, transform);
                packButtons[i][j].Init(categories[i].packs[j].name,i,j,this);
            }
        }

    }

    public LevelButton levelButtonPrefab;
    LevelButton[] levelButtons;

    public void showLevels(int catId , int packId)
    {
        levelButtons = new LevelButton[150];
        for (int i =0; i <150;i++)
        {
            levelButtons[i] = Instantiate(levelButtonPrefab, levelContent.transform);
            //levelButtons[i].Init();
        }
    }
}
