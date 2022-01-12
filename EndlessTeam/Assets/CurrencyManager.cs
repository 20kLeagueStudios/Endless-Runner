using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{

    private static bool savedAlreadyOnce = false;

    //variabili per le valute di gioco
    public int monete, gemme;

    //riferimento al GameManager
    [SerializeField]
    private GameManager gm = default;
    //riferimento all'InventoryManager
    [SerializeField]
    private InventoryManager im = default;


    private void Start()
    {
        //inizializza i valori a quelli salvati    
        Debug.Log("Soldi" + gm.currentMoney);
        monete = gm.currentMoney;
        gemme = gm.currentGems;
        //aggiorna i testi delle value
        im.UpdateCurrencyTexts();

        savedAlreadyOnce = false;

    }
    /*
    private void OnApplicationQuit()
    {
        //se lo stato delle valute non è già stato salvato, viene salvato
        if (!savedAlreadyOnce)
        {
            //salva i dati nel GameManager
            gm.currentMoney = monete;
            gm.currentGems = gemme;
            SaveSystem.Saving(gm);
            savedAlreadyOnce = true;

        }
    
    }*/

    private void OnDestroy()
    {
        //se lo stato delle valute non è già stato salvato, viene salvato
        if (!savedAlreadyOnce)
        {
            //salva i dati nel GameManager
            gm.currentMoney = monete;
            gm.currentGems = gemme;
            SaveSystem.Saving(gm);
            savedAlreadyOnce = true;

        }

    }

}
