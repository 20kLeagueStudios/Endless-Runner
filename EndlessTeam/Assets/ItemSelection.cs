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

        // PlayerPrefs.SetInt("Skin",indexSkin);

        //Debug.Log("mainmenu" + mainMenu.itemShop[1].name);


        if (mydelegate != null)
        {
            mydelegate(this.transform.parent.root.transform.GetChild(7).GetComponent<MainMenu>().itemShop[indexSkin]);
            Debug.Log("CURRENCY= " + currencyManager.currency);

           // SetInt("PlayerSkin", indexSkin);



        }

    }

    public void BuyItem(ItemsShopSO item)
    {
        int tempCurrency = currencyManager.currency;
        int _itemCost = item.itemCost;

        if (currencyManager.currency > 0) 
        {
           
            currencyManager.currency -= item.itemCost;
            
        }
       
        if (currencyManager.currency - item.itemCost < 0)
        {
            currencyManager.currency = tempCurrency;
            Debug.Log("MONEY FINITI");

        }


    }

    public void SetInt(ItemsShopSO item)
    {
        string _string = item.itemType.ToString();
        int Value = this.transform.parent.root.transform.GetChild(7).GetComponent<MainMenu>().itemShop[indexSkin].itemCost;

        PlayerPrefs.SetInt(_string, Value);
    }


    public void AddItem(ItemsShopSO item)
    {
        string _string = item.itemType.ToString();
        int Value = indexSkin;

        Debug.Log(_string);
        Debug.Log("value"+Value);

        PlayerPrefs.SetInt(_string, Value);
    }


    // Start is called before the first frame update
    void Start()
    {
        mainMenu = this.transform.parent.root.transform.GetChild(7).GetComponent<MainMenu>();

        currencyManager = this.transform.parent.root.transform.GetChild(7).GetComponent<CurrencyManager>();

        currency = currencyManager.currency;

         mydelegate += BuyItem;
         mydelegate += AddItem;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CURRENCY= " + currency);

      //  Debug.Log("parent " + this.transform.parent.root.transform.GetChild(7).name);

    }
}
