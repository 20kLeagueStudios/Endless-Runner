﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public MainMenu mainmenu;
 
    public List<ItemsShopSO> itemsAcquistati;
    public GameObject inventoryButtonPrefab;
    public GameObject inventoryButtonContainer;

    public List<ItemsShopSO> defaultItems;


    public ItemsShopSO skinBodySelected;
    public ItemsShopSO skinGambraDestraSelected;
    public ItemsShopSO skinGambaSinistraselected;
    public ItemsShopSO skinBraccioDestroSelected;
    public ItemsShopSO skinBraccioSinistroSelected;
    public ItemsShopSO skinVetroSelected;

    public ItemsShopSO pilotaSelected;

    public List<ItemsShopSO> gadgetSelected; //PENSO DA RIMUOVERE

    public CurrencyManager shop;
    public Text currencyText;

    public GameObject previewTutaPrefab;

    public SkinnedMeshRenderer skinnedTutaMesh;

    public Transform pilotaPosition;

    public float previewRotationSpeed;

    public bool caricaDati=true;

    public List<int> itemListIndex; 

    public List<ItemsShopSO> itemDisponibili;  

    public AudioManager audioManager;

    public GameObject player; //////////////////////

    public Material[] matArray;



    //public void PopulateSaveData(SaveData saveData)
    //{
    //    saveData.items = this.itemsAcquistati;
    //}

    //void SaveJsonData()
    //{
    //    SaveData saveData = new SaveData();

    //    this.PopulateSaveData(saveData);
    //    Debug.Log("SALVA " + saveData.items.Count);

    //    if (FileManager.WriteToFile("SaveData1", saveData.ToJson()))
    //    {


    //    }

    //}


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        matArray = new Material[player.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().sharedMaterials.Length];
 
        currencyText.text = shop.currency.ToString();
        //tutaMesh = previewTutaPrefab.GetComponent<MeshRenderer>();
        //skinnedTutaMesh = previewTutaPrefab.GetComponent<SkinnedMeshRenderer>();

       

        if (caricaDati)
        {

            LoadData();
        }

    }

    public void LoadData()
    {
        SaveData temp = SaveSystem.Loading();

        if (temp != null)
        {
            Debug.Log("CANARINO");
            for (int i = 0; i < temp.itemListIndex.Count; i++) 
            {
                itemListIndex.Add(temp.itemListIndex[i]);
            }

        }

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Saveatstart()
    {
        SaveSystem.Saving(this);
    }

    void OnApplicationQuit()
    {
        SaveSystem.Saving(this);
    }


    private void Start()
    {

        audioManager.PlaySound("MusicaMenu");
       

        if (!itemListIndex.Contains(defaultItems[0].itemSO.id))
        {

            itemListIndex.Add(defaultItems[1].itemSO.id);
            itemListIndex.Add(defaultItems[0].itemSO.id);
        }

        foreach (int  id in itemListIndex)
        {

            for (int i = 0; i < itemDisponibili.Count; i++) //////////////////////////////////////////////////////
            {

                if (id == itemDisponibili[i].itemSO.id)
                {
                    itemsAcquistati.Add(itemDisponibili[i]);

                }

            }

        }

        foreach (ItemsShopSO item in itemsAcquistati)
        {
          
            GameObject inventoryButton = Instantiate(inventoryButtonPrefab) as GameObject;
            inventoryButton.GetComponent<Image>().sprite = item.itemSO.itemImage;
            inventoryButton.transform.SetParent(inventoryButtonContainer.transform, false);

            inventoryButton.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemSO.itemName;
            inventoryButton.transform.GetChild(1).GetComponent<TMP_Text>().text = item.itemSO.itemCost.ToString();

        }
    }



    // Update is called once per frame
    void Update()
    {
        previewTutaPrefab.transform.Rotate(0, previewRotationSpeed * Time.deltaTime, 0);

    }
 

}

