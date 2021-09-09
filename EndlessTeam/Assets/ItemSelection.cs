using System.Collections;
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
        //int tempMoney = currencyManager.monete;
        //int tempGems = currencyManager.gemme;
        Debug.Log("MONETE PRIMA DELL'ACQUISTO = " + currencyManager.monete);
        Debug.Log("GEMME PRIMA DELL'ACQUISTO = " + currencyManager.gemme);
        //int _itemCost = item.itemSO.itemCost;
      

        if (currencyManager.monete > 0 && shopped==false) 
        {
            if (!inventory.itemsAcquistati.Contains(item)) //if aggiuntoora
            {
                Debug.Log("Tipo di valuta per la skin selezionata" + item.itemSO.currencyType);
                //(GABRIELE)
                //se questa skin deve essere presa con monete...
                if (item.itemSO.currencyType == 0)
                {
                    //...se la skin non è già stata comprata e il giocatore ha abbastanza monete...
                    if (shopped == false && currencyManager.monete - item.itemSO.itemCost > 0)
                    {
                        //...la skin viene comprata e vengono diminuite le monete in possesso del giocatore
                        currencyManager.monete -= item.itemSO.itemCost;
                        shopped = true;
                        Debug.Log("Comprata skin con monete");
                    }
                    Debug.Log("Controllo acquisto per monete");
                }
                else //altrimenti, deve essere presa con le gemme, quindi...
                {
                    //...se la skin non è già stata comprata e il giocatore ha abbastanza monete...
                    if (shopped == false && currencyManager.gemme - item.itemSO.itemCost > 0)
                    {
                        //...la skin viene comprata e vengono diminuite le gemme in possesso del giocatore
                        currencyManager.gemme -= item.itemSO.itemCost;
                        shopped = true;
                        Debug.Log("Comprata skin con gemme");
                    }
                    Debug.Log("Controllo acquisto per gemme");
                }
                //se la skin è stata comprata, la aggiunge all'inventario
                if (shopped)
                {

                    AddItem(item);
                    AddIndexItem(item);


                    GameObject inventoryButton = Instantiate(inventory.inventoryButtonPrefab) as GameObject;
                    inventoryButton.transform.GetChild(2).GetComponent<Image>().sprite = item.itemSO.itemImage;
                    inventoryButton.transform.SetParent(inventory.inventoryButtonContainer.transform, false);

                    inventoryButton.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemSO.itemName;
                    inventoryButton.transform.GetChild(1).GetComponent<TMP_Text>().text = item.itemSO.itemCost.ToString();

                    inventory.moneyText.text = (currencyManager.monete).ToString();

                    inventory.gemsText.text = (currencyManager.gemme).ToString();
                    //inventoryButton.GetComponent<Button>().interactable = false;
                    Debug.Log("La skin comprata è stata aggiunta all'inventario");
                }
                /*
                if (currencyManager.monete - item.itemSO.itemCost > 0 && shopped == false)
                {
                    currencyManager.monete -= item.itemSO.itemCost;

                    tempMoney = currencyManager.monete;

                    shopped = true;

                    
                    AddItem(item); ///////////////////////////////////////////////////
                    AddIndexItem(item);


                    GameObject inventoryButton = Instantiate(inventory.inventoryButtonPrefab) as GameObject;
                    inventoryButton.transform.GetChild(2).GetComponent<Image>().sprite = item.itemSO.itemImage; 
                    inventoryButton.transform.SetParent(inventory.inventoryButtonContainer.transform, false);

                    inventoryButton.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemSO.itemName;
                    inventoryButton.transform.GetChild(1).GetComponent<TMP_Text>().text = item.itemSO.itemCost.ToString();

                    inventory.currencyText.text = (tempMoney).ToString();  

                    //inventoryButton.GetComponent<Button>().interactable = false; ////

                }
                else
                {
                    Debug.Log("POLPETTA= ");
                    currencyManager.monete = tempMoney;
                    //inventory.currencyText.text = tempCurrency.ToString(); ///
                }
                */
            }
     
        }

        Debug.Log("MONETE DOPO ACQUISTO = " + currencyManager.monete);
        Debug.Log("GEMME DOPO ACQUISTO = " + currencyManager.gemme);

        Debug.Log("SHOPPED= " + shopped);
        //(GABRIELE)
        //infine, la variabile per lo shop viene riportata al valore originale per i prossimi acquisti
        shopped = false;

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

        currencyManager = this.transform.parent.root.transform.GetChild(6).GetComponent<CurrencyManager>();

         mydelegate += BuyItem;
        // mydelegate += AddItem;
        // mydelegate += Accessorio;

    }


}
