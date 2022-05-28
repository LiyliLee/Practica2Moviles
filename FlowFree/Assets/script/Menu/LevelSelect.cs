using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public HorizontalLayoutGroup horizontalLayout = null;
    public TextMeshProUGUI _packName = null;

    //public LevelGroup _levelPrefab = null;

    private const int LEVELS_PER_PAGE = 30;
    private int _nLevels = 150;
    private string _packageName;
    //private LevelGroup[] _levels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateLevelButtons()
    {
        int totalGroups = _nLevels / LEVELS_PER_PAGE;
        //_levels = new LevelGroup[totalGroups];

        for (int i = 0; i < totalGroups; i++)
        {
            //group = Instantiate(_levelGroupPrefab, _levelLayout.transform);
            //group.SetLevelSelection(this);
            //group.SetButtonColor(_lotColor);
            //group.SetButtonNumbers(1 + LEVELS_PER_GROUP * i, _completedLevelsMarkers);

            //_levels[i] = group;
        }
    }

    public void DeleteLevelButtons()
    {
        int totalGroups = _nLevels / LEVELS_PER_PAGE;

        for (int i = 0; i < totalGroups; i++)
        {
            //if (_levels[i].gameObject != null)
            //Destroy(_levels[i].gameObject);
        }

        //_levels = null;
    }
}
