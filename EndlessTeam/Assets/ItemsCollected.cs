using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemsCollected : MonoBehaviour
{

    public InventoryManager inventory;
    public List<ItemsShopSO> copy;

    private void OnEnable()
    {



        /*
     foreach (ItemsShopSO item in inventory.itemsAcquistati)
     {
         GameObject inventoryButton = Instantiate(inventory.inventoryButtonPrefab) as GameObject;
         inventoryButton.GetComponent<Image>().sprite = item.itemImage;
         inventoryButton.transform.SetParent(inventory.inventoryButtonContainer.transform, false);

         inventoryButton.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemName;
         inventoryButton.transform.GetChild(1).GetComponent<TMP_Text>().text = item.itemCost.ToString();

     }
        */
        /*
        for (int i = 0; i < inventory.itemsAcquistati.Count; i++)
        {
            PlayerPrefs.SetInt("Tuta", i);

            Debug.Log("ELENCO set " + PlayerPrefs.GetInt("Tuta"));
        }
        */
    }

    private void Awake()
    {
        foreach (ItemsShopSO item in inventory.defaultItems)
        {
            GameObject inventoryButton = Instantiate(inventory.inventoryButtonPrefab) as GameObject;
            inventoryButton.GetComponent<Image>().sprite = item.itemImage;
            inventoryButton.transform.SetParent(inventory.inventoryButtonContainer.transform, false);

            inventoryButton.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemName;
            inventoryButton.transform.GetChild(1).GetComponent<TMP_Text>().text = item.itemCost.ToString();
        }
    }

 
    // Start is called before the first frame update
    void Start()
    {   /*
        for (int i = 0; i < inventory.itemsAcquistati.Count; i++)
        {
            PlayerPrefs.GetInt("Tuta");
            Debug.Log("ELENCO get" + PlayerPrefs.GetInt("Tuta"));
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
