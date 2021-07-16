using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public PowerUpsManager powerupsManager;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(this);
        }

    }    
        
    void Start()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
