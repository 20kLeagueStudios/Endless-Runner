using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerDownHandler
{
    public int indexSkin;

    public delegate void OnClickDelegate(ItemsShopSO so);

    OnClickDelegate mydelegate2;

    MainMenu mainMenu;
    CurrencyManager currencyManager;
    InventoryManager inventory;


    public void OnPointerDown(PointerEventData eventData)
    {

        indexSkin = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetSiblingIndex();
        Debug.Log("SELEZIONATO" + " " + indexSkin);


        if (mydelegate2 != null )
        {
            mydelegate2(InventoryManager.instance.itemsAcquistati[indexSkin]);

        }

    }

    public void EquipItemSkin(ItemsShopSO item)
    {

        string _string = item.itemType.ToString();
        int Value = indexSkin;

        // inventory.itemsAcquistati.Add(item);
        if (_string == "Tuta")
        {
            inventory.itemSelected.Clear();

            Debug.Log(_string);
            Debug.Log("value" + Value);

            inventory.itemSelected.Add(item); ////

            PlayerPrefs.SetInt(_string, Value);
        }

    }

    public void EquipItemAccessorio(ItemsShopSO item)
    {
        string _string = item.itemType.ToString();
        if (_string == "Accessorio")
        {
            inventory.gadgetSelected.Clear();

            inventory.gadgetSelected.Add(item); ////
        }


           
           
    }

     

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = this.transform.parent.root.transform.GetChild(7).GetComponent<MainMenu>();
        // inventory = this.transform.parent.root.transform.GetChild(8).GetComponent<InventoryManager>(); //


        inventory = InventoryManager.instance;

        currencyManager = this.transform.parent.root.transform.GetChild(7).GetComponent<CurrencyManager>();

        mydelegate2 += EquipItemSkin;
        mydelegate2 += EquipItemAccessorio;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
