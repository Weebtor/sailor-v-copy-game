using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    // public AudioSource[] narratorSource;

    [Header("Audio Clips")]
    public Sound[] musicClips;
    public Sound[] sfxClips;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }




    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicClips, sound => sound.name == name);
        if (sound == null)
        {
            Debug.Log($"<color=orange>{name} not found</color>");
            return;
        }
        musicSource.clip = sound.clip;
        musicSource.Play();
    }
    public void PlaySfx(string name)
    {
        Sound sound = Array.Find(sfxClips, sound => sound.name == name);
        if (sound == null)
        {
            Debug.Log($"<color=orange>{name} not found</color>");
            return;
        }
        sfxSource.clip = sound.clip;
        sfxSource.Play();
    }



}
