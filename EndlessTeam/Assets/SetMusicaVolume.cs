using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetMusicaVolume : MonoBehaviour
{

    public AudioMixer audioMixerMusica;
    [SerializeField] bool isSFX;

    public void SetVol(float sliderValue)
    {
        if(isSFX==false)
        audioMixerMusica.SetFloat("MusicaVol", Mathf.Log10(sliderValue)*20);

        else audioMixerMusica.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);

    }


}
