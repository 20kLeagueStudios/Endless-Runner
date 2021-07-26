using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem : MonoBehaviour
{
    public enum Language
    {
        English,
        Italian
    }

    public static Language language = Language.Italian;

    public static Dictionary<string, string> localisedEN;
    public static Dictionary<string, string> localisedIT;

    public static bool isInit;

    public static void Init()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        localisedEN = csvLoader.GetDictionaryValues("en");
        localisedIT = csvLoader.GetDictionaryValues("it");

        isInit = true;
    }

    public static string GetLocalisedValue(string key)
    {
        if (!isInit) { Init(); }

        string value = key;

        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Italian:
                localisedIT.TryGetValue(key, out value);
                break;
        }

        return value;
    }
}