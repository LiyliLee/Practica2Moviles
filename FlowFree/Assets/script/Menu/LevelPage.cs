using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPage : MonoBehaviour
{
    public TextMeshProUGUI _packNumber;
    public TextMeshProUGUI[] _levelNumbers = new TextMeshProUGUI[30];
    public Image[] _buttons = new Image[30];
    public Sprite _completeSprite;
    public Sprite _perfectSprite;

    private LevelSelect _levelSelect;

    public void Init(Color c, int level, PlayerData.PassedLevelInfo[] _passedMarkers, LevelSelect levelSelect)
    {
        _levelSelect = levelSelect;

        _packNumber.text = level.ToString() + " - " + (level + 29).ToString();

        GameObject go = new GameObject("Passed Image");

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].color = c;

            _levelNumbers[i].text = (level + i).ToString();


            if (_passedMarkers[level + i - 1] == PlayerData.PassedLevelInfo.PASSED)
            {
                GameObject passedImage = Instantiate(go, _buttons[i].transform);
                passedImage.AddComponent<Image>();
                passedImage.GetComponent<Image>().sprite = _completeSprite;
            }

            else if (_passedMarkers[level + i - 1] == PlayerData.PassedLevelInfo.PERFECT)
            {
                GameObject passedImage = Instantiate(go, _buttons[i].transform);
                passedImage.AddComponent<Image>();
                passedImage.GetComponent<Image>().sprite = _perfectSprite;
            }
        }

        Destroy(go);
    }

    public void LevelButtonClicked(TextMeshProUGUI buttonText)
    {
        int level = int.Parse(buttonText.text);
        _levelSelect.LoadLevel(level);
    }
}
