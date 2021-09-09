using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct Biomes
    {
        public string name;
        public Material[] mat;
    }
    [System.Serializable]
    public struct OtherMaterials
    {
        public string name;
        public Material[] mat;
    }

    public List<GameObject> toReactive = new List<GameObject>();

    public static int portal = -1;

    public Biomes[] biomes;
    public OtherMaterials[] othersMat;

    public
    GameObject playerGb;

    [SerializeField]
    Image gemsImg;

    [SerializeField]
    TextLanguageChange retryText;

    int respawnCount = 0;

    public bool playerDeath = false, preDeath = false;

    [SerializeField]
    TextMeshProUGUI moneyText;

    [SerializeField]
    GameObject countDown;

    [SerializeField]
    TextMeshProUGUI scoreText;

    public int currentScore = 0;

    public Vector3 initialPlayerPos = default;

    public TextLanguageChange dropdownText;

    public int currentMoney, currentGems;

    public int moneyInMatch = 0;

    public static GameManager instance = null;

    public PowerUpsManager powerupsManager;

    public int savedLanguage = 0;

    public bool firstGame = true;

    bool inTutorial = false;

    public float speed = 36;
    public float maxSpeed = 76;
    public int currentScene = -1;
    public bool firstPortal = true;

    public Vector3 portalPos;
    
    public GameObject[] suggestions;

    Dictionary<int, bool> sceneDict = new Dictionary<int, bool>();

    [SerializeField] GameObject parentTiles;

    [SerializeField] Shader startShader;

    public AudioManager audioManager;

    //Enum sulla difficoltà
    enum Mode
    {
        Easy,
        Medium,
        Hard
    }


    //Riferimento all'enum Mode
    Mode mode;


    public string GetMode()
    {
        return mode.ToString();
    }

    public void IncreaseDifficulty()
    {
        if (mode == Mode.Easy) mode = Mode.Medium;
        else if (mode == Mode.Medium) mode = Mode.Hard;

    }

    public void ResetDifficulty()
    {
        mode = Mode.Easy;
    }

    public void DecreaseDifficulty()
    {
        if (mode == Mode.Medium) mode = Mode.Easy;
        else if (mode == Mode.Hard) mode = Mode.Medium;

    }

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

        Time.timeScale = 1;

        mode = Mode.Easy;
    }

    void Start()
    {
        if(audioManager) audioManager.PlaySound("LivelloFunghi");

        if(parentTiles) parentTiles.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = startShader;
         
        LoadScene(2);
         
        if(moneyText) moneyText.text = ": " + currentMoney.ToString();

        if(playerGb) initialPlayerPos = playerGb.transform.position;

        LoadData();
       
    }

    private void LoadData()
    {
        //SaveData temp = SaveSystem.Loading();
        SaveData temp = SaveSystem.LoadingGameManager();
        if (temp != null)
        {
            currentMoney = temp.money;
            currentGems = temp.gems;
            savedLanguage = temp.savedLanguage;
            if(moneyText) moneyText.text = currentMoney.ToString();
        }
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
        pauseButton.SetActive(true);
        if (!inTutorial)
        {
            Time.timeScale = 1;
            
        }
    }

    [SerializeField] GameObject pauseButton;
    public void Pause()
    {
        pauseButton.SetActive(false);
        //Le prime due righe servono per controllare se già il tempo è fermato a causa del tutorial
        if (Time.timeScale == 0) inTutorial = true;
        else { inTutorial = false; audioManager.PlaySound("Pulsante_1"); }
        Time.timeScale = 0;
    }

    public void IncreaseMoney()
    {
        currentMoney++;
        moneyInMatch++;
        moneyText.text = ": " + currentMoney.ToString();
    }

    public void IncreaseScore(int value)
    {
        currentScore += value;
        if (currentScore < 0) currentScore = 0;
        scoreText.text = currentScore.ToString();

    }

    //public void PopulateSaveData(SaveData saveData)
    //{
    //    saveData.money = this.currentMoney;
    //    saveData.savedLanguage = this.savedLanguage;

    //}

    //public void LoadFromSaveData(SaveData saveData)
    //{
    //    this.currentMoney = saveData.money;
    //    this.savedLanguage = saveData.savedLanguage;
    //}

    //public void SaveJsonData()
    //{
    //    SaveData saveData = new SaveData();
    //    this.PopulateSaveData(saveData);

    //    if (FileManager.WriteToFile("SaveData0", saveData.ToJson())) {
    //    }
        
    //}

    //void LoadJsonData()
    //{
    //    if (FileManager.LoadFromFile("SaveData0", out var jsonFile))
    //    {
    //        SaveData saveData = new SaveData();
    //        saveData.LoadFromJson(jsonFile);
    //        currentMoney = saveData.money;
    //        this.savedLanguage = saveData.savedLanguage;
    //    }
    //}

    void OnApplicationQuit()
    {
        
        SaveSystem.Saving(this); // FIXATO

    }

    public void Respawn()
    {
        ResetDifficulty();
        Time.timeScale = 1;
        playerGb.GetComponent<PlayerMovement>().Resurrection();
        GameObject.FindObjectOfType<ObjectPooling>().CheckPointOffset();
        //ObjectPooling.instance.CheckPointOffset();

        retryText.GetComponent<TextMeshProUGUI>().fontSize = 5.2f;
        retryText.UpdateText("Gems to retry!", "Gemme per riprovare!");
        gemsImg.gameObject.SetActive(true);
        
    }

    public void StartCountDown()
    {
        StartCoroutine("CountDownCor");
    }

    IEnumerator CountDownCor()
    {
        countDown.SetActive(true);
        TextMeshProUGUI countDownText = countDown.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        playerGb.GetComponent<PlayerMovement>().animator.Play("Running");
        float initialSize = countDownText.fontSize;
        Time.timeScale = 0;
        //float desiredSize = 190f;
        speed = 0;
        for (int i = 3; i >= 1; i--)
        {
            countDownText.text = i.ToString();
            float time = Time.unscaledTime;
            while (Time.unscaledTime - time < 1)
            {
                countDownText.fontSize += 1f;
                yield return null;
            }
            time = Time.unscaledTime;
            countDownText.fontSize = initialSize;

        }
        countDown.SetActive(false);
        playerDeath = false;
        preDeath = false;
        speed = 36;
        Time.timeScale = 1;
        yield return null;
    }


}