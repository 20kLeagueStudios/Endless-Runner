using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniitialMaterial : MonoBehaviour
{
    private void Awake()
    {
        if (!GameManager.instance.firstGame)
            Material();
    }

    void Material()
    {
        Renderer[] temp = transform.GetComponentsInChildren<Renderer>();
        Debug.Log("SEEEEE" + temp.Length);
        foreach (Renderer rend in temp)
        {
            rend.enabled = false;

        }
    }
}
