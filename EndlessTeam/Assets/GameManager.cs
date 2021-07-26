using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour, ISaveable
{
    [SerializeField]
    TextMeshProUGUI moneyText;

    [SerializeField]
    TextLanguageChange scoreText;

    int currentScore = 0;

    public TextLanguageChange dropdownText;

    public int currentMoney;

    public static GameManager instance = null;

    public PowerUpsManager powerupsManager;

    public int savedLanguage = 0;

    public bool firstGame = true;

    public float speed = 36;
    public float maxSpeed = 76;
    public int currentScene = -1;
    public bool firstPortal = true;
    
    public GameObject[] suggestions;

    Dictionary<int, bool> sceneDict = new Dictionary<int, bool>();

    [SerializeField] GameObject parentTiles;

    [SerializeField] Shader startShader;


    public GameObject GetObjFromArray(string name, GameObject[] array)
    {
        foreach (GameObject temp in array)
        {
            if (temp.name == name) return temp;

        }

        return null;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        LoadJsonData();

        moneyText.text = ": " + currentMoney.ToString();
    }



    void Start()
    {
        parentTiles.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = startShader;

        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
        LoadScene(2);
    }

    public void LoadScene(int scene)
    {
        if (!sceneDict.ContainsKey(scene))
        {
            sceneDict.Add(scene, true);
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
        else
        {
            if (sceneDict[scene] == false)
            {
                sceneDict[scene] = true;
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
        }


    }

    public void DeactivateScene(int scene)
    {
        if (sceneDict[scene] == true)
        {
            sceneDict[scene] = false;
            SceneManager.UnloadSceneAsync(scene);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void IncreaseMoney()
    {
        currentMoney += 1;
        moneyText.text = ": " + currentMoney.ToString();
    }

    public void IncreaseScore(int value)
    {
        currentScore += value;
        if (currentScore < 0) currentScore = 0;
        scoreText.UpdateText("Score: " + currentScore, "Punti: " + currentScore);

    }

    public void PopulateSaveData(SaveData saveData)
    {
        saveData.money = currentMoney;
        saveData.savedLanguage = this.savedLanguage;
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        currentMoney = saveData.money;
        this.savedLanguage = saveData.savedLanguage;
    }

    void SaveJsonData()
    {
        SaveData saveData = new SaveData();
        this.PopulateSaveData(saveData);

        if (FileManager.WriteToFile("SaveData0", saveData.ToJson())) { 
            //Salvataggio eseguito
        }
        
    }

    void LoadJsonData()
    {
        if (FileManager.LoadFromFile("SaveData0", out var jsonFile))
        {
            SaveData saveData = new SaveData();
            saveData.LoadFromJson(jsonFile);
            currentMoney = saveData.money;
            this.savedLanguage = saveData.savedLanguage;
        }
    }

    void OnApplicationQuit()
    {
        SaveJsonData();
    }
}