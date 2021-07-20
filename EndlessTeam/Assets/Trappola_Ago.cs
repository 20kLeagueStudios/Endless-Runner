using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trappola_Ago : MonoBehaviour
{
    [SerializeField]
    private Animator aim;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.CompareTag("Player"))
        {
            aim.SetTrigger("DARDI");
            
        }
       
    }
}
