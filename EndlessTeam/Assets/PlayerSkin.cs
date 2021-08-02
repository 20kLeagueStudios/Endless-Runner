using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] ItemsShopSO[] skin;
    [SerializeField] ItemsShopSO[] gadget;

    [SerializeField] GameObject player;
    [SerializeField] GameObject accessorio;

    public MeshFilter accessorioMesh;
    public MeshRenderer accessorioMeshRenderer;



    void Start()
    {

            skin = InventoryManager.instance.itemSelected.ToArray();
            gadget = InventoryManager.instance.gadgetSelected.ToArray();

       

            player.GetComponent<MeshRenderer>().material.mainTexture = skin[0].playerSkin;

        if (gadget[0].itemName != "DEFULT ACCESSORIO")
        {
            SetGadget();
        }
            

    }

    public void SetGadget()
    {
        accessorio.SetActive(true);
        accessorioMesh.mesh= gadget[0].accessorio.GetComponent<MeshFilter>().sharedMesh;
        accessorioMeshRenderer.material = gadget[0].accessorioMaterial;
        accessorio.transform.localScale = gadget[0].accessorio.transform.localScale;


    }

    // Update is called once per frame
    void Update()
    {

    }

}
