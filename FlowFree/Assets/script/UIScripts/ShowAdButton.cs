using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ShowAdButton : MonoBehaviour
{

    string gameId = "4508761";

    void Start()
    {
        Advertisement.Initialize(gameId);
    }

    public void ShowAd()
    {
        Advertisement.Show();
    }
}
