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

    public CurrencyManager shop;
    public Text currencyText;

    public GameObject previewTutaPrefab;
    public MeshRenderer tutaMesh;

    public Transform accessorioPosition;
    public MeshFilter accessorioMesh;
    public MeshRenderer accessorioMeshRenderer;

    public float previewRotationSpeed;

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


        currencyText.text = shop.currency.ToString();
        tutaMesh = previewTutaPrefab.GetComponent<MeshRenderer>();

    }

   
    private void Start()
    {
        defaultItems.Add(itemSelected[0]);
        defaultItems.Add(gadgetSelected[0]);


        foreach (ItemsShopSO item in defaultItems)
        {
            itemsAcquistati.Add(item);


            GameObject inventoryButton = Instantiate(inventoryButtonPrefab) as GameObject;
            inventoryButton.GetComponent<Image>().sprite = item.itemImage;
            inventoryButton.transform.SetParent(inventoryButtonContainer.transform, false);

            inventoryButton.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemName;
            inventoryButton.transform.GetChild(1).GetComponent<TMP_Text>().text = item.itemCost.ToString();

        }
    }

  

    // Update is called once per frame
    void Update()
    {
        previewTutaPrefab.transform.Rotate(0, previewRotationSpeed * Time.deltaTime, 0);
    }

}
