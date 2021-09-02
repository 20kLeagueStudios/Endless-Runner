using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    AudioSource source;
    public AudioMixerGroup audioMixerGroup;
    public string clipName;
    public AudioClip audioClip;

    public float volume;
    public float pitch;

    public bool loop = false;
    public bool playOnAwake = false;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = audioClip;
        source.pitch = pitch;
        source.loop = loop;
        source.volume = volume;
        source.playOnAwake = playOnAwake;
        source.outputAudioMixerGroup = audioMixerGroup;

    }

    public void Play()
    {
        source.Play();
    }

    public IEnumerator LerpMusic(string _name)
    {
        float currentVol = source.volume;
        float currentTime = 0;

        float duration = 1.5f;

        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(1, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);

            source.volume= Mathf.Log10(newVol) * 20;

            yield return null;

        }


        yield break;
    }public IEnumerator LerpMusicRev(string _name)
    {
        float currentVol = source.volume;
        float currentTime = 0;

        float duration = 1.5f;

        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(1, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(targetValue, currentVol, currentTime / duration);

            source.volume= Mathf.Log10(newVol) * 20;

            yield return null;

        }


        yield break;
    }


}

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    Sound[] sound;

    private void Awake()
    {
        for (int i = 0; i < sound.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sound[i].clipName);
            _go.transform.SetParent(this.transform);
            sound[i].SetSource(_go.AddComponent<AudioSource>());
        }

    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if(sound[i].clipName == _name)
            {
                sound[i].Play();
                return;
            }
        }
    }

    public void Lerpa(string _name)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].clipName == _name)
            {
              StartCoroutine( sound[i].LerpMusic(sound[i].clipName));

                return;
            }
        }
        
    }

    public void Slerpa(string _name)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].clipName == _name)
            {
                StartCoroutine(sound[i].LerpMusicRev(sound[i].clipName));

                return;
            }
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Lerpa("LivelloFunghi");
            PlaySound("LivelloLava");
            Slerpa("LivelloLava");
            
        }
    }



}
