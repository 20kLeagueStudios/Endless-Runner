using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public int money, savedLanguage;

    public List<ItemsShopSO> items;///

    //Ritorna la classe in formato json
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    //Carica un file json già esistente e lo sovrascrive a questa istanza
    public void LoadFromJson(string file) 
    {
        JsonUtility.FromJsonOverwrite(file, this);
    }

   
}

public interface ISaveable
{
    void PopulateSaveData(SaveData saveData);
    void LoadFromSaveData(SaveData saveData);
}