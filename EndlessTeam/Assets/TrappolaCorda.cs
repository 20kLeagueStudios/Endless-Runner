using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrappolaCorda : MonoBehaviour
{
    public GameObject corda;
    public GameObject tronco;
    [SerializeField]
    private Animator aim;
    void Start()
    {
        corda.SetActive(true);
        tronco.SetActive(false);
    }


   void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            corda.SetActive(false);
            tronco.SetActive(true);
            aim.SetTrigger("Trigger_Tronco");
      
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
