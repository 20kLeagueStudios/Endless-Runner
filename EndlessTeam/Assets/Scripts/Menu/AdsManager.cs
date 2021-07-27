using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    private string gameId = "4215264";
#elif UNITY_ANDROID
    private string gameId = "4215265";
#endif


    bool testMode = true;
    string mySurfacingId = "Interstitial_Android";
    public FortuneWheel FortuneWh;

    void Start()
    {
        Advertisement.AddListener(this);
        // Inizializza gli annunci:
        //Advertisement.Initialize(gameId, testMode);
    }

    public void ShowInterstitialAd()
    {
        // Controlla se UnityAds è pronto prima di chiamare il metodo Show :
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
            // Sostituisci mySurfacingId con l'id with the ID dei posizionamenti che desideri visualizzare (come mostrato nella tua Unity Dashboard)
        }
        else
        {
            Debug.Log("Interstitial Ads non è pronto al momento! Per favore riprova più tardi!");
        }
    }

    public void ShowRewardedVideo()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(mySurfacingId))
        {
            Advertisement.Show(mySurfacingId);
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            if(gameObject.tag == "Wheel")
            {
                FortuneWh.GiraRuota();
            }
            // Reward the user for watching the ad to completion.
            Debug.Log("hai guadagnato 30.000 euro");
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
            Debug.Log("niente soldi, hai skippato.");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == mySurfacingId)
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
