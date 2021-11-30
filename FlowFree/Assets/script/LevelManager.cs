using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    protected struct LevelData
    {
        public int width, height;
        public int numFlows;

        public List<int>[] solutions_;

        public Vector2[] emptys_;
        public Vector2[] wall_;
    }

    public void CreateLevel(TextAsset pack, int levelToPlay)
    {
        LevelData levelData;

        string packText = pack.text;

        string[] levels = packText.Split('\n');

        string[] level = levels[levelToPlay].Split(';');
        string[] cabecera = level[0].Split(',');

        //Cabecera
        string tamanho = cabecera[0];

        string[] tam = tamanho.Split(':');
        if(tam.Length == 1)
        {
            levelData.width = int.Parse(tam[0]);
            levelData.height = int.Parse(tam[0]);
        } else {
            levelData.width = int.Parse(tam[0]);
            levelData.height = int.Parse(tam[1]);
        }

        levelData.numFlows = int.Parse(cabecera[3]);

        string[] walls = level[levelData.numFlows].Split(':');

        //Flows
        for (int i = 0; i < levelData.numFlows; i++)
        {

        }
    }
}
