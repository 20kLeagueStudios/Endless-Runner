using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] ItemsShopSO[] skins;
    [SerializeField] GameObject player;

    int indexBodySkin;

    void Start()
    {
        indexBodySkin = PlayerPrefs.GetInt("Corpo");

        player.GetComponent<MeshRenderer>().material.mainTexture = skins[indexBodySkin].playerSkin;
        Debug.Log("indice " + indexBodySkin);
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
