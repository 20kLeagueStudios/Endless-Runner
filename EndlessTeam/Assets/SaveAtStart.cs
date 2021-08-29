using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAtStart : MonoBehaviour
{
    public InventoryManager inventoryManager;

    void Saveatstart()
    {
        SaveSystem.Saving(inventoryManager);
    }

}
