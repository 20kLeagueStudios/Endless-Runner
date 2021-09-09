using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{

    private static bool saveOnce = false;

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
        monete = gm.currentMoney;
        gemme = gm.currentGems;
        //aggiorna i testi delle value
        im.UpdateCurrencyTexts();

        saveOnce = false;

    }

    private void OnDestroy()
    {
        if (saveOnce)
        {
            //salva i dati nel GameManager
            gm.currentMoney = monete;
            gm.currentGems = gemme;
            SaveSystem.Saving(gm);
            saveOnce = true;

        }
    
    }

}
