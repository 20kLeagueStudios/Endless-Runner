using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStarter : MonoBehaviour
{
    public enum PowerUpsEnum 
    {
        ChangeGravityStarter,
        MiniStarter,
        DashStarter
    };

    [Header("Seleziona un powerup da attivare")]

    [SerializeField] PowerUpsEnum powerupDaAttivare = PowerUpsEnum.ChangeGravityStarter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.powerupsManager.canUsePowerUp)
        {
            GameManager.instance.powerupsManager.PowerUpActive(powerupDaAttivare.ToString());
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
