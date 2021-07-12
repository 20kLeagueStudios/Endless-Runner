using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{

    #if UNITY_IOS
    private string gameId = "";
    #elif UNITY_ANDROID
    private string gameId = "";
#endif


    bool testMode = true;

    void Start()
    {
        // Inizializza gli annunci:
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowInterstitialAd()
    {
        // Controlla se UnityAds è pronto prima di chiamare il metodo Show :
        if (Advertisement.IsReady())
        {
            Advertisement.Show("mySurfacingId");
            // Sostituisci mySurfacingId con l'id with the ID dei posizionamenti che desideri visualizzare (come mostrato nella tua Unity Dashboard)
        }
        else
        {
            Debug.Log("Interstitial Ads non è pronto al momento! Per favore riprova più tardi!");
        }
    }
}
