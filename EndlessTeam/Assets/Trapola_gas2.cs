using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapola_gas2 : MonoBehaviour
{
    [SerializeField]
    private Animator aim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.CompareTag("GG"))
        {
            aim.SetTrigger("Trigger_Gas");
        }
        
    }
}
