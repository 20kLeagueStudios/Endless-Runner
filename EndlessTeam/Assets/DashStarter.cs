using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashStarter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.powerupsManager.canUsePowerUp)
        {
            GameManager.instance.powerupsManager.CallCoroutineDash();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
