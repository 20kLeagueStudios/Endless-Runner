using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    GameObject[] toDeactivate = default;

    [SerializeField]
    TextLanguageChange[] statText = default;

    [SerializeField]
    PlayerHealth playerHealth = default;

    

    private void OnEnable()
    {

        if (GameManager.instance.deathCounter <= 2)
        {
            GameManager.instance.retryText.UpdateText("Look Ads to revive!", "Guarda una pubblicità per resuscitare!");
        }
        else
        {
            GameManager.instance.retryText.UpdateText("Gem to retry!", "Gemma per riprovare!");
            GameManager.instance.gemsImg.gameObject.SetActive(true);
        }
        for (int i = 0; i < toDeactivate.Length - 1; i++) toDeactivate[i].SetActive(false);

        statText[0].UpdateText("Score: " + GameManager.instance.currentScore, "Punti: " + GameManager.instance.currentScore);
        statText[1].UpdateText(": " + GameManager.instance.moneyInMatch, ": " + GameManager.instance.moneyInMatch);
    }

    private void OnDisable()
    {
        for (int i = 0; i < toDeactivate.Length - 1; i++) toDeactivate[i].SetActive(true);
        playerHealth.ResetHealth();
    }

    public void Resurrection()
    {
        GameManager.instance.Respawn();
        //gameObject.SetActive(false);
        
    }

    

}
