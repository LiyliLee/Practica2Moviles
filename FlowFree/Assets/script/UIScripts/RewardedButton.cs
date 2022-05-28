using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedButton : MonoBehaviour, IUnityAdsListener
{
    private string placementId = "Rewarded_Android";
    private string gameId = "4508761";

    public Button button_;

    BannerPosition bannerPos_ = BannerPosition.BOTTOM_CENTER;

    void Start()
    {
        // Set interactivity to be dependent on the Placement’s status:
        button_.interactable = Advertisement.IsReady(placementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (button_) button_.onClick.AddListener(ShowAd);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);

        Advertisement.Banner.SetPosition(bannerPos_);
    }

    void ShowAd()
    {
        Advertisement.Show(placementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string plId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (plId == placementId)
        {
            button_.interactable = true;
        }
    }

    public void OnUnityAdsDidStart(string message)
    {
        Debug.Log("Ad starting");
    }

    public void OnUnityAdsDidFinish(string plId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            GameManager._instance.AddHint();
        }
        else
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogWarning("The ad did not finish due to an error.");
    }

}
