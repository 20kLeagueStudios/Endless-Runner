using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityStarter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.powerupsManager.canUsePowerUp)
        {
            GameManager.instance.powerupsManager.ChangeGravity();
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
