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
        foreach (Renderer rend in temp)
        {
            rend.enabled = false;

        }

        Invoke("MaterialFix",1);
    }

    void MaterialFix()
    {
        Renderer[] rendererFirst = transform.GetComponentsInChildren<Renderer>();

        for (int i = 0; i < rendererFirst.Length; i++)
        {
            Renderer temp = rendererFirst[i];

            bool tags = temp.CompareTag("Money") || temp.CompareTag("Enemy") || temp.CompareTag("Portal") || temp.CompareTag("PortalShader") || temp.CompareTag("PowerUp");
            if (!tags)
            {
                if (!temp.CompareTag("Invisible")) temp.enabled = true;
            }
            else if (temp.CompareTag("Money")) {  temp.enabled = true; }
            else if (temp.CompareTag("Enemy")) { temp.enabled = true; }
            else if (temp.CompareTag("Portal")) { temp.enabled = true; }
            else if (temp.CompareTag("PowerUp")) { temp.enabled = true; }
        }
    }
}
