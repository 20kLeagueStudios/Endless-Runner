using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    #region json
    //public static bool WriteToFile(string a_FileName, string a_FileContents)
    //{
    //    var fullPath = Path.Combine(Application.streamingAssetsPath, a_FileName);

    //    try
    //    {
    //        File.WriteAllText(fullPath, a_FileContents);
    //        return true;
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError($"Failed to write to {fullPath} with exception {e}");
    //        return false;
    //    }
    //}

    //public static bool LoadFromFile(string a_FileName, out string result)
    //{
    //    //var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);
    //    var fullPath = DataPath(a_FileName);
    //    //var fullPath = "Assets/Resources/SaveData0";
    //    WWW reader = new WWW(fullPath);
    //    while (!reader.isDone) { } // Do nothing
    //    string dataString = reader.text;
    //    try
    //    {
    //        result = File.ReadAllText(dataString);
    //        //result = temp.ToString();
    //        return true;
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError($"Failed to read from {fullPath} with exception {e}");
    //        result = "";
    //        return false;
    //    }
    //}

    //public static string DataPath(string jsonName)
    //{
    //    if (Directory.Exists(Application.persistentDataPath))
    //    {
    //        return Path.Combine(Application.persistentDataPath, jsonName);
    //    }
    //    return Path.Combine(Application.streamingAssetsPath, jsonName);
    //}
    #endregion

    public static void Saving(GameManager gm)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        //string path = Application.persistentDataPath + "/SaveData.sav";
        string path = Application.persistentDataPath + "/SaveDataGM.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(gm);
        formatter.Serialize(stream, data);

        stream.Close();

        Debug.LogError("SALVATI DATI CON GAMEMANAGER: " + gm.currentMoney);

    }

    public static void Saving(InventoryManager inv)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //string path = Application.persistentDataPath + "/SaveData.sav";
        string path = Application.persistentDataPath + "/SaveDataINV.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(inv);
        formatter.Serialize(stream, data);

        stream.Close();
        //Debug.LogError("SALVATI DATI CON INVENTORYMANAGER");
    }
    

    public static SaveData LoadingGameManager()
    {
        string path = Application.persistentDataPath + "/SaveDataGM.sav";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;

            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    
        public static SaveData Loading()
    {
        //string path = Application.persistentDataPath + "/SaveData.sav";
        string path = Application.persistentDataPath + "/SaveDataINV.sav";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;

            stream.Close();

            return data;
        } 
        else 
        {
            //Debug.LogError("Save file not found in " + path);
            return null;
        }

    }
}