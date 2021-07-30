using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] ItemsShopSO[] skin;
    [SerializeField] ItemsShopSO[] gadget;

    [SerializeField] GameObject player;


    void Start()
    {

            skin = InventoryManager.instance.itemSelected.ToArray();
            gadget = InventoryManager.instance.gadgetSelected.ToArray();

            player.GetComponent<MeshRenderer>().material.mainTexture = skin[0].playerSkin;     

    }

    // Update is called once per frame
    void Update()
    {

    }

}
