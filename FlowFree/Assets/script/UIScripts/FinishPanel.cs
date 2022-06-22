using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;

public class FinishPanel : MonoBehaviour
{

    string gameId = "4508761";

    public TMP_Text perfectText_;
    public TMP_Text stepsText_;

    void Start()
    {
        Advertisement.Initialize(gameId);
    }

    public void ShowAd()
    {
        Advertisement.Show();
    }

    public void SetFinishPanel(bool perfect, int steps)
    {
        if (perfect) perfectText_.text = "¡Perfecto!";
        else perfectText_.text = "¡Nivel completado!";

        stepsText_.text = "Completaste el nivel en " + steps + " pasos.";

        ShowAd();
    }
}
