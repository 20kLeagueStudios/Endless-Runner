using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] ItemsShopSO[] skin;
    [SerializeField] GameObject pilota;

    [SerializeField] GameObject player;

    [SerializeField] GameObject pilotaPosition = default;
    //[SerializeField] GameObject accessorio;

    //public MeshFilter accessorioMesh;
    //public MeshRenderer accessorioMeshRenderer;



    void Awake()
    {

        // skin = InventoryManager.instance.itemSelected.ToArray();
        if (InventoryManager.instance != null)
        {
            if (InventoryManager.instance.pilotaSelected != null)
            {
                pilota = Instantiate(InventoryManager.instance.pilotaSelected.itemSO.pilota, pilotaPosition.transform.position, pilotaPosition.transform.rotation, pilotaPosition.transform);
            }
        }
       

            //player.GetComponent<MeshRenderer>().material.mainTexture = skin[0].itemSO.playerSkin;

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


}
