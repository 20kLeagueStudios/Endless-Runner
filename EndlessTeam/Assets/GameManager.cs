using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public PowerUpsManager powerupsManager;

    public float speed = 36;
    public float maxSpeed = 76;

    [SerializeField]GameObject parentTiles;

    [SerializeField] Shader startShader;



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
        parentTiles.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = startShader;

        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
