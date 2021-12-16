using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    string gameId = "4508761";
    string bannerId = "Banner_Android";
    bool testMode = true;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }
    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(bannerId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerId);
    }

}
