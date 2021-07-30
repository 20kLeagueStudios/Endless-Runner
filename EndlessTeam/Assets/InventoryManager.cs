using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public MainMenu mainmenu;
 
    public List<ItemsShopSO> itemsAcquistati;
    public GameObject inventoryButtonPrefab;
    public GameObject inventoryButtonContainer;

    public List<ItemsShopSO> defaultItems;


    public List<ItemsShopSO> itemSelected;
    public List<ItemsShopSO> gadgetSelected;

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

        //itemSelected = mainmenu.a;
 


    }

   


    void OnEnable()
    {
        //itemsShopped = itemsAcquistati.ToArray();

        //itemeSelected = mainmenu.a;


        Debug.Log("ITEMACQUISTATI " + itemsAcquistati.Count);

    }




    private void Start()
    {
        defaultItems.Add(itemSelected[0]);
        defaultItems.Add(gadgetSelected[0]);

        foreach (ItemsShopSO item in defaultItems)
        {
            itemsAcquistati.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        /*
        for (int i = 0; i < itemsAcquistati.Count; i++)
        {
            PlayerPrefs.SetInt("Tuta"+i, i);

            Debug.Log("ELENCO get" + PlayerPrefs.GetInt("Tuta"+i));
        }
        */
    }

}
