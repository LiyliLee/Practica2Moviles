using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPage : MonoBehaviour
{
    public TextMeshProUGUI _packNumber; // intervalo de los niveles por pagina
    public Image[] _buttons = new Image[30]; // array con los 30 botones por pagina
    public TextMeshProUGUI[] _levelNumbers = new TextMeshProUGUI[30]; // texto de cada uno de los botones
    public Sprite _passedSprite; // nivel completado
    public Sprite _perfectSprite; // nivel perfecto

    private LevelSelect _levelSelect;

    public void Init(Color c, int level, PlayerData.PassedLevelInfo[] _passedMarkers, LevelSelect levelSelect)
    {
        _levelSelect = levelSelect;

        _packNumber.text = level.ToString() + " - " + (level + 29).ToString();

        GameObject go = new GameObject("Passed Image");

        // Añade a cada uno de los botones la informacion para que se muestren correctamente
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].color = c;

            _levelNumbers[i].text = (level + i).ToString();

            // si el nivel ha sido completado se crea el sprite correspondiente
            if (_passedMarkers[level + i - 1] == PlayerData.PassedLevelInfo.PASSED)
            {
                GameObject passedImage = Instantiate(go, _buttons[i].transform);
                passedImage.AddComponent<Image>();
                passedImage.GetComponent<Image>().sprite = _passedSprite;
            }
            // lo mismo si se ha completado con el minimo numero de pasos
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
        // Manda la informacion del nivel que ha sido pulsado al selector de niveles
        int level = int.Parse(buttonText.text);
        _levelSelect.LoadLevel(level);
    }
}
