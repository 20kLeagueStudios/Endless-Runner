using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextLanguageChange dropdownText; 

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
    }



    void Start()
    {
        parentTiles.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = startShader;

        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
        LoadScene(1);
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

    public void HandleDropDown(int value)
    {
        if (value == 0) dropdownText.UpdateText("Italiano", "Italian");
        if (value == 1) dropdownText.UpdateText("Inglese", "English");
    }
}