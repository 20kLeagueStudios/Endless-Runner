using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerWalking : MonoBehaviour
{

    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameManager.instance.audioManager;
    }

    public void Walking()
    {
        audioManager.PlaySound("Passo");
    }
}
