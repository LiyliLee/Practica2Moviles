using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public RectTransform _scrollContent = null;
    public VerticalLayoutGroup _packLayout= null;
    public GameObject _packPanel = null;
    //public LevelSelection _levelSelectionPanel = null;

    public GameObject _categoryUIPrefab = null;

    private CategoryLevel[] _levelCategories;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(CategoryLevel[] categoryLevels)
    {
        _levelCategories = categoryLevels;

        CreateCategoryObjects();
    }

    private void CreateCategoryObjects()
    {
        int nCategories = _levelCategories.Length;

        CreateButtons(nCategories);
    }

    private void CreateButtons(int nButtons)
    {
        GameObject button;

        for(int i = 0; i < nButtons; i++)
        {
            button = Instantiate(_categoryUIPrefab, _packLayout.transform);

            CategoryLevel cl = _levelCategories[i];
            CategoryTitle category = button.GetComponent<CategoryTitle>();

            category.Init(cl.color, cl.name);
            category.SetPacks(cl, _packLayout);
        }
    }
}
