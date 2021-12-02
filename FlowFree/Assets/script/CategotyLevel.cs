using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Categoty", menuName ="Category")]
public class CategotyLevel : ScriptableObject
{
    public PackLevel[] packs;
    public string categoryName;
    public Color color;

}
