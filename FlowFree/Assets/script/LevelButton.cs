using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Fondo1, Fondo2;
    public Text titulo;
    //public GameObject botonesNiveles;

    public string tit;
    public Color color;

    void Start()
    {
        
        Fondo1.color = new Color(color.r,color.g,color.b,0.5f);
        Fondo2.color = color;
        
        titulo.text = tit;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
