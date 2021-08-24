using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum ItemType
{
    Tuta,
    Pilota,
    Accessorio
}

[CreateAssetMenuAttribute(fileName ="ItemShop", menuName = "ItemShop/ItemSO")]
public class ItemsShopSO : ScriptableObject
{
    /*
    public string itemName;
    public Sprite itemImage;
    public Texture playerSkin;
    public int itemCost;
    public GameObject accessorio;
    public Material accessorioMaterial;

    public ItemType itemType = default;
    */

    public ItemSerializzato itemSO;

}

[Serializable]
public class ItemSerializzato
{
    public string itemName;

    public int id;

    public Sprite itemImage;
    public Texture playerSkin;
    public int itemCost;
    public GameObject accessorio;
    public Material accessorioMaterial;

    public ItemType itemType = default;

}