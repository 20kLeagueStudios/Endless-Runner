using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessorioScript : MonoBehaviour
{
    int accessorio;

    void Awake()
    {
         accessorio = PlayerPrefs.GetInt("Accessorio");
        Debug.Log("aaa"+accessorio);

        if (accessorio == 1)
        {
            Debug.Log("accessorio player" + PlayerPrefs.GetInt("Accessorio"));

            this.gameObject.SetActive(true);

        }
        else
        {
            this.gameObject.SetActive(false);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
