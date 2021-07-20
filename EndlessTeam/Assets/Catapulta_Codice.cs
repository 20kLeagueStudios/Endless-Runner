using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapulta_Codice : MonoBehaviour
{
    [SerializeField]
    private Animator aim;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.CompareTag("Player"))
        {
            aim.SetTrigger("Trigger_catapulta");
        }

    }
}
