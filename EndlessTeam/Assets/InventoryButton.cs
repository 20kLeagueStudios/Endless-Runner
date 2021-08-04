using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    public int indexSkin;

    public delegate void OnClickDelegate(ItemsShopSO so);

    OnClickDelegate mydelegate2;

    MainMenu mainMenu;
    CurrencyManager currencyManager;
    InventoryManager inventory;

 
    public void OnPointerClick(PointerEventData eventData)
    {

        indexSkin = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetSiblingIndex();

 
        foreach (Transform item in inventory.inventoryButtonContainer.transform)
        {
           item.GetComponent<Button>().interactable = true;

            Debug.Log("PERA " + eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Button>().gameObject.name);
        }
       
        eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Button>().interactable = false;


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


           // PlayerPrefs.SetInt(_string, Value);
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

    public void ChangePreviewTexture(ItemsShopSO item)
    {
        string _string = item.itemType.ToString();
        if (_string == "Tuta")
        {
            inventory.tutaMesh.material.mainTexture = item.playerSkin;
        }
        else if (_string == "Accessorio")
        {
            if (item.accessorio != null)
            {
                inventory.accessorioMeshRenderer.enabled = true;

                // GameObject accessorio = Instantiate(item.accessorioMesh, inventory.accessorioPosition.transform.position, Quaternion.identity) as GameObject;
                inventory.accessorioMesh.mesh = item.accessorio.GetComponent<MeshFilter>().sharedMesh;
                inventory.accessorioMeshRenderer.material = item.accessorioMaterial;
                inventory.accessorioMesh.transform.localScale = item.accessorio.transform.localScale;
                Debug.Log("CIAO");
            }
            else { 

                inventory.accessorioMeshRenderer.enabled = false;
                inventory.accessorioMeshRenderer.material = null;
                Debug.Log("POLLO");
                return; 
            }
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
        mydelegate2 += ChangePreviewTexture;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
