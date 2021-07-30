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
        int tempCurrency = currencyManager.currency;
        int _itemCost = item.itemCost;
      

        if (currencyManager.currency > 0 && shopped==false) 
        {

            if (currencyManager.currency - item.itemCost > 0 && shopped == false)
            {
                currencyManager.currency -= item.itemCost;

                tempCurrency = currencyManager.currency;

                shopped = true;

                AddItem(item);

                GameObject inventoryButton = Instantiate(inventory.inventoryButtonPrefab) as GameObject;
                inventoryButton.GetComponent<Image>().sprite = item.itemImage;
                inventoryButton.transform.SetParent(inventory.inventoryButtonContainer.transform, false);

                inventoryButton.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemName;
                inventoryButton.transform.GetChild(1).GetComponent<TMP_Text>().text = item.itemCost.ToString();
            }
            else
            {
                Debug.Log("POLPETTA= ");
                currencyManager.currency = tempCurrency;
            }
        }

        Debug.Log("CURRENCY= " + currencyManager.currency);
        Debug.Log("temp= " + tempCurrency);

        Debug.Log("SHOPPED= " + shopped);


    }

    /*
    public void SetInt(ItemsShopSO item)
    {
        string _string = item.itemType.ToString();
        int Value = this.transform.parent.root.transform.GetChild(7).GetComponent<MainMenu>().itemShop[indexSkin].itemCost;

        PlayerPrefs.SetInt(_string, Value);
    }
    */

    public void Accessorio(ItemsShopSO item)
    {
        //string _string = "Accessorio";
        if (item.itemType == ItemType.Accessorio)
        {
            inventory.itemsAcquistati.Add(item);

            //item.accessorioMesh.SetActive(true);
            // PlayerPrefs.SetInt(_string, 1);
            Debug.Log("accessorio " + PlayerPrefs.GetInt("Accessorio"));
        }
       
    }

    public void AddItem(ItemsShopSO item)
    {
        string _string = item.itemType.ToString();
        int Value = indexSkin;

        inventory.itemsAcquistati.Add(item);



        Debug.Log(_string);
        Debug.Log("value"+Value);

        //PlayerPrefs.SetInt(_string, Value);
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


    void Update()
    {

    }


    
}
