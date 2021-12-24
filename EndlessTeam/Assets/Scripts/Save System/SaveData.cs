using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int money = 0, savedLanguage = 0;


    // public List<ItemSerializzato> items = new List<ItemSerializzato>(); /////
    public List<int> itemListIndex = new List<int>();

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
            for (int i = 0; i < inv.itemListIndex.Count; i++)
       {
            //Debug.Log(inv.itemListIndex[i]);
            this.itemListIndex.Add(inv.itemListIndex[i]); 

        }
    }
}

//public interface ISaveable
//{
//    void PopulateSaveData(SaveData saveData);
//    void LoadFromSaveData(SaveData saveData);
//}