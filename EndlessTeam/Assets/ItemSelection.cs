using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class ItemSelection : MonoBehaviour, IPointerClickHandler
{
    public int indexSkin;
    int currency;

    public delegate void OnClickDelegate(ItemsShopSO so);

    OnClickDelegate mydelegate;

    MainMenu mainMenu;
    CurrencyManager currencyManager;


    public void OnPointerClick(PointerEventData eventData)
    {
        indexSkin = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetSiblingIndex();
        Debug.Log("SELEZIONATO" + " " + indexSkin);


        if (mydelegate != null)
        {
            mydelegate(this.transform.parent.root.transform.GetChild(7).GetComponent<MainMenu>().itemShop[indexSkin]);
        

        }

    }

    public void BuyItem(ItemsShopSO item)
    {
        int tempCurrency = currencyManager.currency;
        int _itemCost = item.itemCost;

        if (currencyManager.currency > 0) 
        {
            tempCurrency = currencyManager.currency;

            currencyManager.currency -= item.itemCost;


        }
 
        if (currencyManager.currency - item.itemCost < 0)
        {
            currencyManager.currency = tempCurrency;

        }
     
        Debug.Log("CURRENCY= " + currencyManager.currency);

    }

    public void SetInt(ItemsShopSO item)
    {
        string _string = item.itemType.ToString();
        int Value = this.transform.parent.root.transform.GetChild(7).GetComponent<MainMenu>().itemShop[indexSkin].itemCost;

        PlayerPrefs.SetInt(_string, Value);
    }

    public void Accessorio(ItemsShopSO item)
    {
        string _string = "Accessorio";
        if (item.itemType == ItemType.Accessorio)
        {
            
            //item.accessorioMesh.SetActive(true);
            PlayerPrefs.SetInt(_string, 1);
            Debug.Log("accessorio " + PlayerPrefs.GetInt("Accessorio"));
        }
        else
        {
            PlayerPrefs.SetInt(_string, 0);
        }
    }

    public void AddItem(ItemsShopSO item)
    {
        string _string = item.itemType.ToString();
        int Value = indexSkin;

        Debug.Log(_string);
        Debug.Log("value"+Value);

        PlayerPrefs.SetInt(_string, Value);
    }



    void Start()
    {
        mainMenu = this.transform.parent.root.transform.GetChild(7).GetComponent<MainMenu>();

        currencyManager = this.transform.parent.root.transform.GetChild(7).GetComponent<CurrencyManager>();

        currency = currencyManager.currency;

         mydelegate += BuyItem;
         mydelegate += AddItem;
         mydelegate += Accessorio;

    }


    void Update()
    {

    }


    
}
