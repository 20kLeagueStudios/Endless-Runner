﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSelection : MonoBehaviour, IPointerClickHandler
{
    public int indexSkin;
 
    public delegate void OnClickDelegate(ItemsShopSO so);

    OnClickDelegate mydelegate;

    MainMenu mainMenu;
    CurrencyManager currencyManager;
    InventoryManager inventory;

    bool shopped = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        indexSkin = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetSiblingIndex();
        Debug.Log("SELEZIONATO" + " " + indexSkin);


        if (mydelegate != null && shopped==false)
        {
            mydelegate(this.transform.parent.root.transform.GetChild(2).GetComponent<MainMenu>().itemShop[indexSkin]);
        
        }

    }

    public void BuyItem(ItemsShopSO item)
    {
        int tempCurrency = currencyManager.currency;
        int _itemCost = item.itemSO.itemCost;
      

        if (currencyManager.currency > 0 && shopped==false) 
        {
            if (!inventory.itemsAcquistati.Contains(item)) //if aggiuntoora
            {
                if (currencyManager.currency - item.itemSO.itemCost > 0 && shopped == false)
                {
                    currencyManager.currency -= item.itemSO.itemCost;

                    tempCurrency = currencyManager.currency;

                    shopped = true;

                    
                    AddItem(item); ///////////////////////////////////////////////////
                    AddIndexItem(item);


                    GameObject inventoryButton = Instantiate(inventory.inventoryButtonPrefab) as GameObject;
                    inventoryButton.transform.GetChild(2).GetComponent<Image>().sprite = item.itemSO.itemImage; 
                    inventoryButton.transform.SetParent(inventory.inventoryButtonContainer.transform, false);

                    inventoryButton.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemSO.itemName;
                    inventoryButton.transform.GetChild(1).GetComponent<TMP_Text>().text = item.itemSO.itemCost.ToString();

                    inventory.currencyText.text = (tempCurrency).ToString();  

                    //inventoryButton.GetComponent<Button>().interactable = false; ////

                }
                else
                {
                    Debug.Log("POLPETTA= ");
                    currencyManager.currency = tempCurrency;
                    //inventory.currencyText.text = tempCurrency.ToString(); ///
                }
            }
     
        }

        Debug.Log("CURRENCY= " + currencyManager.currency);
        Debug.Log("temp= " + tempCurrency);

        Debug.Log("SHOPPED= " + shopped);


    }

   


    /*
    public void Pilota(ItemsShopSO item)
    {
        //string _string = "Pilota";
        if (item.itemSO.itemType == ItemType.Pilota)
        {
            inventory.itemsAcquistati.Add(item);

            //item.pilota.SetActive(true);
            // PlayerPrefs.SetInt(_string, 1);
            Debug.Log("accessorio " + PlayerPrefs.GetInt("Accessorio"));
        }
       
    }
    */

    public void AddIndexItem(ItemsShopSO item)
    {
        inventory.itemListIndex.Add(item.itemSO.id);
    }

    public void AddItem(ItemsShopSO item)
    {
        string _string = item.itemSO.itemType.ToString();
        int Value = indexSkin;
        inventory.audioManager.PlaySound("PulsanteAcquisti");
        inventory.itemsAcquistati.Add(item);

        Debug.Log(_string);
        Debug.Log("value"+Value);

    }



    void Start()
    {
        mainMenu = this.transform.parent.root.transform.GetChild(2).GetComponent<MainMenu>();
        // inventory= this.transform.parent.root.transform.GetChild(8).GetComponent<InventoryManager>(); //

        inventory = InventoryManager.instance;

        currencyManager = this.transform.parent.root.transform.GetChild(7).GetComponent<CurrencyManager>();

         mydelegate += BuyItem;
        // mydelegate += AddItem;
        // mydelegate += Accessorio;

    }


}
