﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
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
    public Image gemsImg = default;

    public TextLanguageChange retryText = default;

    public Renderer boss;

    //int respawnCount = 0;

    public bool playerDeath = false, preDeath = false;

    [SerializeField]
    TextMeshProUGUI moneyText = default, gemsText = default;

    [SerializeField]
    GameObject countDown = default;

    [SerializeField]
    TextMeshProUGUI scoreText = default;

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

    public MakePortalVisible currentPortal;
    
    public GameObject[] suggestions;

    Dictionary<int, bool> sceneDict = new Dictionary<int, bool>();

    [SerializeField] GameObject parentTiles = default;

    [SerializeField] Shader startShader = default;

    public AudioManager audioManager;
    public AudioMixer audioMixerMusica;

    public float volumeMusicaIniziale;

    //riferimento allo script di comportamento del boss
    [SerializeField]
    private BossBehaviour bb = default;

    //Numero di volte in cui il giocatore è morto
    public int deathCounter = 0;

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
        //Imposto il frame rate a 60
        Application.targetFrameRate = 60;

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

        savedLanguage= PlayerPrefs.GetInt("savedLanguage");

        LoadData();

    }

    void Start()
    {
        //Carico tutte le scene e attivo solo quella dei funghi
        if (gameObject.scene.buildIndex == 1)
        {
            LoadAllScene();
            audioManager.PlaySound("LivelloFunghi");

            parentTiles.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = startShader;

            moneyText.text = ": " + currentMoney.ToString();

            initialPlayerPos = playerGb.transform.position;

        }

    }

    private void LoadAllScene()
    {
        //Carico la scena dei funghi e la tengo attiva
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        //Carico tutte le altre scene
        for(int i=3; i<= 6; i++)
        {
            SceneManager.LoadScene(i, LoadSceneMode.Additive);
            //e le disattivo
            StartCoroutine(DeactivateScene(i));
        }
    }

    private void LoadData()
    {
        //SaveData temp = SaveSystem.Loading();
        SaveData temp = SaveSystem.LoadingGameManager();
        if (temp != null)
        {
            currentMoney = temp.money;
            currentGems = temp.gems;
            //Debug.Log("monete " + temp.money + "/n" + "gemme " + temp.gems);
            // savedLanguage = temp.savedLanguage;
            if (moneyText) moneyText.text = currentMoney.ToString();
            if (gemsText) gemsText.text = currentGems.ToString();
        }
    }

    /// <summary>
    /// Metodo che carica una scena e lo inserisce in un dizionario che contiene le scene che le attiva
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene(int scene)
    {
        //Ottengo la scena corrente
        Scene currentScene = SceneManager.GetSceneByBuildIndex(scene);

        //Ottengo tutti i GB della scena corrente 
        GameObject[] allGBScene = currentScene.GetRootGameObjects();

        //Li looppo tutti
        for (int i = 0; i < allGBScene.Length; i++)
        {
            //e li attivo se sono disattivi
            if (!allGBScene[i].activeSelf) allGBScene[i].SetActive(true);
        }


    }

 
    /// <summary>
    /// Metodo che disattiva una scena
    /// </summary>
    /// <param name="scene"></param>
    public IEnumerator DeactivateScene(int scene)
    {
        //Ottengo la scena corrente
        Scene currentScene = SceneManager.GetSceneByBuildIndex(scene);

        while (!currentScene.isLoaded) 
        {
            yield return null;
        }


        //Ottengo tutti i GB della scena corrente 
        GameObject[] allGBScene = currentScene.GetRootGameObjects();

        //Li looppo tutti
        for (int i=0; i<allGBScene.Length; i++)
        {
            //e li disattivo se sono attivi
            if (allGBScene[i].activeSelf) allGBScene[i].SetActive(false);
        }
        yield return null;
    }

    public void ResumeGame()
    {
        pauseButton.SetActive(true);
        if (!inTutorial)
        {
            Time.timeScale = 1;
            
        }
    }

    /// <summary>
    /// Metodo che ritorna una booleana che indica se la pausa è attiva o no
    /// </summary>
    /// <returns></returns>
    public bool PauseIsActivated()
    {
        return !pauseButton.activeSelf;
    }

    [SerializeField] GameObject pauseButton = default;
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
        if (currentPortal != null && currentPortal.gameObject.activeSelf)
        {
            Debug.Log(currentPortal.gameObject.name);
            currentPortal.DisablePortal();
        }

        retryText.GetComponent<TextMeshProUGUI>().fontSize = 5.2f;


        //Aumento il counter delle volte che il giocatore è morto
        deathCounter++;
        //Chiamo metodo del boss che lo riposiziona
        bb.PlayerRespawned();


    }

    /// <summary>
    /// Metodo che ritorna se il giocatore è morto un numero di volte a tal punto di non poter più guardare la pubblicità ma di pagare le gemme
    /// </summary>
    /// <returns></returns>
    public bool IsResurrectionGems()
    {
        return deathCounter >= 3;
    }

    public void StartCountDown()
    {
        StartCoroutine("CountDownCor");
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
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