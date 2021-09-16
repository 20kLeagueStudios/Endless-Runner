using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetMusicaVolume : MonoBehaviour
{

    public AudioMixer audioMixerMusica;
    [SerializeField] bool isSFX;
    [SerializeField] Slider slider;

    public float volumeMusica;
    public float volumeSFX;

    [SerializeField] bool isMenu=false;

    public void SetVol(float sliderValue)
    {


        if (isSFX == false) {
            audioMixerMusica.SetFloat("MusicaVol", Mathf.Log10(sliderValue) * 20);
            volumeMusica = sliderValue;
            PlayerPrefs.SetFloat("volumeMusica", volumeMusica);
            Debug.Log("MUSICA " + volumeMusica);
        }

        else { 
            audioMixerMusica.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
            volumeSFX = sliderValue;
            PlayerPrefs.SetFloat("volumeSFX", volumeSFX);
            Debug.Log("SFX " + volumeSFX);
        }


    }

    private void Awake()
    {
        if (!isMenu)
        {
            if (isSFX == false)
            {
                slider.value=PlayerPrefs.GetFloat("volumeMusica");
                SetVol(slider.value );
            }
            else
            {
                slider.value=PlayerPrefs.GetFloat("volumeSFX");
                SetVol(slider.value );
            }
        }
    }

}
