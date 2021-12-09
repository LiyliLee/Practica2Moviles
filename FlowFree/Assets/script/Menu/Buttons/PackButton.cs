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
    Color c;

    PackSelect packSelectContent;
    
    // Start is called before the first frame update
    public void Init(string PackN, int catid, int packid, PackSelect sel)
    {
        packName.text = PackN;
        categoryId= catid;
        packId= packid;
        nivelespasados = GameManager._instance.GetPackunlockeds(catid, packid);
        c= GameManager._instance.GetCategoties()[catid].color;
        packPercentaje.text = nivelespasados + " / " + 150;
        packName.color =c;
    }
    public void showLevels()
    {
        packSelectContent.showLevels(categoryId, packId);
    }
}
