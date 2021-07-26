using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Corpo,
    Gadget
}

[CreateAssetMenuAttribute(fileName ="ItemShop", menuName = "ItemShop/ItemSO")]
public class ItemsShopSO : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public Texture playerSkin;
    public int itemCost;

    public ItemType itemType = default;
    
}
