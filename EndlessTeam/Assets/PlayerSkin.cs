using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] ItemsShopSO[] skin;
    [SerializeField] ItemsShopSO[] pilota;

    [SerializeField] GameObject player;
    //[SerializeField] GameObject accessorio;

    //public MeshFilter accessorioMesh;
    //public MeshRenderer accessorioMeshRenderer;



    void Start()
    {

           // skin = InventoryManager.instance.itemSelected.ToArray();
            pilota = InventoryManager.instance.gadgetSelected.ToArray();

       

            player.GetComponent<MeshRenderer>().material.mainTexture = skin[0].itemSO.playerSkin;

        /*
        if (gadget[0].itemSO.itemName != "DEFULT ACCESSORIO")
        {
            SetGadget();
        }
        */

    }

    /*
    public void SetGadget()
    {
        accessorio.SetActive(true);
        accessorioMesh.mesh= gadget[0].itemSO.accessorio.GetComponent<MeshFilter>().sharedMesh;
        accessorioMeshRenderer.material = gadget[0].itemSO.accessorioMaterial;
        accessorio.transform.localScale = gadget[0].itemSO.accessorio.transform.localScale;


    }
    */

    // Update is called once per frame
    void Update()
    {

    }

}
