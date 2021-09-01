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

}
