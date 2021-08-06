using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sputa_FuocoCODE : MonoBehaviour
{
    [SerializeField]
    private Animator aim;
    [SerializeField]
   ParticleSystem TotFuoco = null;

    // Update is called once per frame
     void Start()
    {
        TotFuoco.Stop();
    }
    void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.CompareTag("Player"))
        {
            aim.SetTrigger("Trigger_Fuoco");
            TotFuoco.Play();
        }

    }
}
