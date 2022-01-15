using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    [SerializeField] CurrencyManager currencyManager;
    [SerializeField] InventoryManager inventorymanager;

    string gems10 = "com.nautilusacademy.gatewayrush.gems10";
    string gems15 = "com.nautilusacademy.gatewayrush.gems15";
    string gems30 = "com.nautilusacademy.gatewayrush.gems30";
    string gems40 = "com.nautilusacademy.gatewayrush.gems40";
    string coins200 = "com.nautilusacademy.gatewayrush.coins200";
    string coins500 = "com.nautilusacademy.gatewayrush.coins500";

    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id == gems10 )
        {
            Debug.Log("Hai acquistato 10 gemme");
            currencyManager.gemme += 10;
           // GameManager.instance.currentGems += 10;
        }
        else if(product.definition.id == gems15)
        {
            Debug.Log("Hai acquistato 15 gemme");
            currencyManager.gemme += 15;
           // GameManager.instance.currentGems += 15;
        }  
        else if(product.definition.id == gems30)
        {
            Debug.Log("Hai acquistato 30 gemme");
            currencyManager.gemme += 30;
           // GameManager.instance.currentGems += 30;
        }
        else if(product.definition.id == gems40)
        {
            Debug.Log("Hai acquistato 40 gemme");
            currencyManager.gemme += 40;
           // GameManager.instance.currentGems += 30;
        } 
        else if(product.definition.id == coins200)
        {
            Debug.Log("Hai acquistato 200 monete");
            currencyManager.monete += 200;
           // GameManager.instance.currentGems += 30;
        }   
        else if(product.definition.id == coins500)
        {
            Debug.Log("Hai acquistato 500 monete");
            currencyManager.monete += 500;
           // GameManager.instance.currentGems += 30;
        }

        inventorymanager.UpdateCurrencyTexts();

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + " fallito perché" + failureReason);
    }



}
