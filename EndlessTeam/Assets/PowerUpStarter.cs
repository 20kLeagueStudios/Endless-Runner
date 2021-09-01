using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStarter : MonoBehaviour
{
    Renderer rend; //per debug
    public enum PowerUpsEnum 
    {
        ChangeGravityStarter,
        MiniStarter,
        DashStarter,
        JumpPowerUp
    };

    [Header("Seleziona un powerup da attivare")]

    [SerializeField] PowerUpsEnum powerupDaAttivare = PowerUpsEnum.ChangeGravityStarter;
    AudioManager audioManager;

    private void Awake()
    {
        rend = GetComponent<Renderer>(); //per debug, coloro la piattaforma in base al tipo di powerup da attivare

        audioManager = GameManager.instance.audioManager;

        if (powerupDaAttivare == PowerUpsEnum.ChangeGravityStarter)
        {
            rend.material.SetColor("_Color", Color.red);
        }

        if (powerupDaAttivare == PowerUpsEnum.DashStarter)
        {
            rend.material.SetColor("_Color", Color.green);
        }

        if (powerupDaAttivare == PowerUpsEnum.MiniStarter)
        {
            rend.material.SetColor("_Color", Color.blue);
        }

        if (powerupDaAttivare == PowerUpsEnum.JumpPowerUp)
        {
            rend.material.SetColor("_Color", Color.gray);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //&& GameManager.instance.powerupsManager.canUsePowerUp
        if (other.CompareTag("Player") )
        {
            GameManager.instance.powerupsManager.PowerUpActive(powerupDaAttivare.ToString());
            audioManager.PlaySound("PowerUp");
        }

    }


}
