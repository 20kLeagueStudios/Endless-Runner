using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    int i = 0;
    void Start()
    {
        
    }

   /* 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.UnloadSceneAsync("Scene01");

            Debug.Log("SCARICA");

        }
    }
    */

    private void OnEnable()
    {
        if (i == 0)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            i++;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
