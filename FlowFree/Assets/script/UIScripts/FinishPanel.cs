using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishPanel : MonoBehaviour
{

    public TMP_Text perfectText_;
    public TMP_Text stepsText_;

    public void SetFinishPanel(bool perfect, int steps)
    {
        if (perfect) perfectText_.text = "¡Perfecto!";
        else perfectText_.text = "¡Nivel completado!";

        stepsText_.text = "Completaste el nivel en " + steps + " pasos.";
    }
}
