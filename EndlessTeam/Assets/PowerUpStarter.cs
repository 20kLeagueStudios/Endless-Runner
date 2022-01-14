using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStarter : MonoBehaviour
{
    Renderer rend; //per debug

    //Array di Materiali da cui verrà scelto il materiale iniziale iniziale, in base al tipo di power up, nello stesso ordine del powerUpsEnum
    [SerializeField]
    private Material[] powerUpMat;
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
        rend = GetComponentInChildren<Renderer>(); //per debug, coloro la piattaforma in base al tipo di powerup da attivare

        //Assegno il materiale seguendo il power up impostato
        rend.material = powerUpMat[(int)powerupDaAttivare];

        audioManager = GameManager.instance.audioManager;

        //if (powerupDaAttivare == PowerUpsEnum.ChangeGravityStarter)
        //{
        //    rend.material.SetColor("_Color", Color.red);
        //}

        //if (powerupDaAttivare == PowerUpsEnum.DashStarter)
        //{
        //    rend.material.SetColor("_Color", Color.green);
        //}

        //if (powerupDaAttivare == PowerUpsEnum.MiniStarter)
        //{
        //    rend.material.SetColor("_Color", Color.blue);
        //}

        //if (powerupDaAttivare == PowerUpsEnum.JumpPowerUp)
        //{
        //    rend.material.SetColor("_Color", Color.gray);
        //}

    }

    /// <summary>
    /// Metodo che ritorna il tipo di power up in valore numerico
    /// </summary>
    /// <returns>0 Gravity, 1 Mini starter, 2 Dash, 3 Salto</returns>
    public int GetPowerUpType() 
    {
        return (int)powerupDaAttivare;
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
