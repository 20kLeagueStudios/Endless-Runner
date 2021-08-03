using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    GameObject[] toDeactivate;

    [SerializeField]
    TextLanguageChange[] statText;

    [SerializeField]
    PlayerHealth playerHealth;

    private void OnEnable()
    {

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
