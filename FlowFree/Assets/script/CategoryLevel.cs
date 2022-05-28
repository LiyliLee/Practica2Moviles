using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Category", menuName ="Category")]
public class CategoryLevel : ScriptableObject
{
    public PackLevel[] packs;
    public string categoryName;
    public Color color;

}
