using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;


public class GameManager : MonoBehaviour, ISaveable
{
    [SerializeField]
    GameObject playerGb;

    [SerializeField]
    Image gemsImg;

    [SerializeField]
    TextLanguageChange retryText;

    int respawnCount = 0;

    public bool playerDeath = false;

    [SerializeField]
    TextMeshProUGUI moneyText;

    [SerializeField]
    GameObject countDown;

    [SerializeField]
    TextLanguageChange scoreText;

    public int currentScore = 0;

    public Vector3 initialPlayerPos = default;

    public TextLanguageChange dropdownText;

    public int currentMoney;

    public int moneyInMatch = 0;

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

    void Update() //////////////////////////////////////////////////////////////
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
        }
    }

    void Start()
    {
        parentTiles.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = startShader;

        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
        LoadScene(2);

        initialPlayerPos = playerGb.transform.position;
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
        currentMoney++;
        moneyInMatch++;
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
        saveData.money = this.currentMoney;
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

    public void Respawn()
    {
        Time.timeScale = 1;
        playerGb.GetComponent<PlayerMovement>().Resurrection();
        ObjectPooling.instance.CheckPointOffset();

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
        float initialSize = countDownText.fontSize;
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
        speed = 36;
        yield return null;
    }
}