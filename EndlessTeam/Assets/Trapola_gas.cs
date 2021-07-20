using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapola_gas : MonoBehaviour
{
    public GameObject inesco;

    //public GameObject tronco;
    [SerializeField]
    private Animator aim;
    [SerializeField]
    private Animator cc;
    [SerializeField]
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        inesco.SetActive(true);
     

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            V_Inesco();
          
         
        }

    }
    void V_Inesco()
    {
        
        inesco.SetActive(false);
        cc.SetTrigger("Trigger_Inesco");
        aim.SetTrigger("Trigger_Gas");
    }

}
