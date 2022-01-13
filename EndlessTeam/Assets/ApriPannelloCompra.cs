using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApriPannelloCompra : MonoBehaviour
{
    [SerializeField] GameObject pannellogemme;

    private void Start()
    {
        pannellogemme.SetActive(false);
    }

    public void Pannellogemme()
    {
        if(!pannellogemme.activeInHierarchy)
        pannellogemme.SetActive(true);

        else if (pannellogemme.activeInHierarchy)
            pannellogemme.SetActive(false);
    }

 

}
