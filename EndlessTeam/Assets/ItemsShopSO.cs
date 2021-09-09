using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum TutaType
{   
    None,
    Busto,
    Vetro,
    Gambe,
    Braccia
}

public enum ItemType
{
    Tuta,
    Pilota,
    
}

[CreateAssetMenuAttribute(fileName ="ItemShop", menuName = "ItemShop/ItemSO")]
public class ItemsShopSO : ScriptableObject
{

    public ItemSerializzato itemSO;

}

[Serializable]
public class ItemSerializzato
{
    public string itemName;

    public int id;

    public Sprite itemImage;
    public Texture playerSkin;


    //(GABRIELE)
    //indica il tipo di valuta da usare per comprare questa skin
    // 0 - Monete
    // 1 - Gemme
    public int currencyType;
    //riferimenti agli sprite per le valute
    public Sprite moneySprite, gemsSprite;

    
    public int itemCost;
    public GameObject pilota;
    public SkinnedMeshRenderer skinnedMeshRender; 
    //public Material accessorioMaterial;

    public ItemType itemType = default;
    public TutaType tutaType = default;

   

}