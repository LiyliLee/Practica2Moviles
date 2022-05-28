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

    // Start is called before the first frame update
    public void Start()
    {
    }

    public void SetPack(string categoryName, string packName, Color c)
    {
        _category = categoryName;
        _packName.text = packName;
        _packName.color = c;

        _completeLevel.text = 0.ToString() + "/" + 150.ToString();
    }

    public void SetCallback(LevelSelect ls)
    {
        _levelSelect = ls;
        _button.onClick.AddListener(ButtonCallback);
    }

    private void ButtonCallback()
    {
        _levelSelect.gameObject.SetActive(true);
        _levelSelect.CreateLevelButtons();
    }
}
