using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public HorizontalLayoutGroup _horizontalLayout = null;
    public TextMeshProUGUI _packNameText = null;

    public LevelPage _levelPagePrefab = null;

    private const int LEVELS_PER_PAGE = 30;
    private int _nLevels = 150;
    private string _categoryName; // nombre de la categoria
    private LevelPage[] _levels; // array de paginas de niveles
    private Color _packColor; // color del pacvk
    private PlayerData.PassedLevelInfo[] _completeMarkers; // array de niveles completos

    public void SetPackData(Color c, string packName, string categoryName, PlayerData.PassedLevelInfo[] passedMarkers)
    {
        // Inicializa los valores en funcion del pack que se ha seleccionado
        _packColor = c;
        _packNameText.text = packName;
        _packNameText.color = c;

        _categoryName = categoryName;

        _completeMarkers = passedMarkers;
    }

    public void InitLevelButtons()
    {
        // Inicializa los botones de seleccion de nivel
        // Hay un total de 150 niveles por pack y se muestran 30 por pagina
        int totalPages = _nLevels / LEVELS_PER_PAGE;
        _levels = new LevelPage[totalPages];

        LevelPage page;
        for (int i = 0; i < totalPages; i++)
        {
            // Instancia cada pagina de niveles
            page = Instantiate(_levelPagePrefab, _horizontalLayout.transform);
            page.Init(_packColor, 1 + LEVELS_PER_PAGE * i, _completeMarkers, this);

            _levels[i] = page;
        }
    }

    public void DeleteLevelButtons()
    {
        // Elimina los botones instanciados
        int totalGroups = _nLevels / LEVELS_PER_PAGE;

        for (int i = 0; i < totalGroups; i++)
        {
            if (_levels[i].gameObject != null)
                Destroy(_levels[i].gameObject);
        }

        _levels = null;
    }

    public void LoadLevel(int level)
    {
        // Carga el nivel seleccionado
        GameManager.GetInstance().SelectedLevelInfo(_categoryName, _packNameText.text, level - 1);
        GameManager.GetInstance().ToLevelScene();
    }
}
