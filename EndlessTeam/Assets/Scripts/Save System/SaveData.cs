using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int money = 0, savedLanguage = 0;


    public List<ItemSerializzato> items = new List<ItemSerializzato>();


    ////Ritorna la classe in formato json
    //public string ToJson()
    //{
    //    return JsonUtility.ToJson(this);
    //}

    ////Carica un file json già esistente e lo sovrascrive a questa istanza
    //public void LoadFromJson(string file) 
    //{
    //    JsonUtility.FromJsonOverwrite(file, this);
    //}

   public SaveData(GameManager gm)
    {
        
        this.money = gm.currentMoney;
        this.savedLanguage = gm.savedLanguage;
        //this.items = InventoryManager.instance.itemsAcquistati;
    }

    public SaveData(InventoryManager inv)
    {
        for(int i=0; i<inv.itemsAcquistati.Count; i++)
        {
            Debug.Log(i);
            this.items.Add(inv.itemsAcquistati[i].itemSO);
        }
        //this.items = InventoryManager.instance.itemsAcquistati;
    }
}

//public interface ISaveable
//{
//    void PopulateSaveData(SaveData saveData);
//    void LoadFromSaveData(SaveData saveData);
//}