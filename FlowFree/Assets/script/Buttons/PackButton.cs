using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackButton : MonoBehaviour
{
    public Text packName;
    public Text packPercentaje;
    int categoryId;
    int packId;
    int nivelespasados;

    PackSelect packSelectContent;
    
    // Start is called before the first frame update
    public void Init(string PackN, int catid, int packid, PackSelect sel)
    {
        packName.text = PackN;
        nivelespasados = GameManager._instance.GetPackunlockeds(catid, packid);
        packPercentaje.text = nivelespasados + " / " + 150;
    }
    public void showLevels()
    {
        packSelectContent.showLevels(categoryId, packId);
    }
}
