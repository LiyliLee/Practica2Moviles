using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PackSelect : MonoBehaviour
{
    public TextMeshProUGUI _packName = null;
    public TextMeshProUGUI _completeLevel = null;
    public Button _button = null;

    private LevelSelect _levelSelect;
    private string _category;
    private PlayerData.PassedLevelInfo[] _passedLevels;

    public void SetPack(string categoryName, string packName, Color c, PlayerData.PassedLevelInfo[] passedInfo)
    {
        _category = categoryName;
        _packName.text = packName;
        _packName.color = c;
        _passedLevels = passedInfo;

        int passed = 0;
        for(int i = 0; i < _passedLevels.Length; i++)
        {
            if (_passedLevels[i] != 0)
                passed++;
        }
        _completeLevel.text = passed.ToString() + "/" + _passedLevels.Length.ToString();
    }

    public void SetCallback(LevelSelect ls)
    {
        _levelSelect = ls;
        _button.onClick.AddListener(ButtonCallback);
    }

    private void ButtonCallback()
    {
        _levelSelect.gameObject.SetActive(true);
        _levelSelect.SetPackData(_packName.color, _packName.text, _category, _passedLevels);
        _levelSelect.InitLevelButtons();
    }
}
