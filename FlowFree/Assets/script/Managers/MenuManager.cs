using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public RectTransform _scrollContent = null;
    public VerticalLayoutGroup _packLayout= null;
    public GameObject _packPanel = null;
    public LevelSelect _levelSelectionPanel = null;

    public GameObject _categoryUIPrefab = null;

    private CategoryLevel _currentCategory;
    private PackLevel _currentPack;
    private CategoryLevel[] _levelCategories;
    private Dictionary<string, PlayerData.PassedLevelInfo[]> _passedInfo;

    public void Init(CategoryLevel currentCategory, PackLevel currentPack, CategoryLevel[] categoryLevels, Dictionary<string, PlayerData.PassedLevelInfo[]> completionInfo)
    {
        _currentCategory = currentCategory;
        _currentPack = currentPack;
        _levelCategories = categoryLevels;
        _passedInfo = completionInfo;

        if (GameManager.GetInstance().FromLevelScene())
        {
            // si venimos de la escena del juego activa el panel de seleccion de nivel
            _packPanel.SetActive(true);

            _levelSelectionPanel.gameObject.SetActive(true);
            _levelSelectionPanel.SetPackData(_currentCategory.color, _currentPack.name,
                _currentCategory.name, _passedInfo[_currentPack.name]);
            _levelSelectionPanel.InitLevelButtons();
        }

        CreateCategoryObjects();
    }

    private void CreateCategoryObjects()
    {
        // crea las categorias con sus respectivos packs
        int nCategories = _levelCategories.Length;

        CreateButtons(nCategories);
    }

    private void CreateButtons(int nButtons)
    {
        // crea los botones de cada uno de los packs de niveles
        GameObject button;

        for(int i = 0; i < nButtons; i++)
        {
            button = Instantiate(_categoryUIPrefab, _packLayout.transform);

            CategoryLevel cl = _levelCategories[i];
            CategoryTitle category = button.GetComponent<CategoryTitle>();

            category.Init(cl.color, cl.name, _levelSelectionPanel);
            category.SetPacks(cl, _packLayout, _passedInfo);
        }
    }
}
