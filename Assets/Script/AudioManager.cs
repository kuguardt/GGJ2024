using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    public float masterSound = 1f; //100.0f;
    public float sfxSound = 1f; //100.0f;
    public float bgmSound = 1f; //100.0f;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        CreateSingleton();
    }

    void CreateSingleton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        if (s == null)
        {
            Debug.LogError("sound name : " + name + " is not found.");
            return;
        }
        s.source.Play();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        if (s == null)
        {
            Debug.LogError("sound name : " + name + " is not found.");
            return;
        }
        s.source.Stop();
    }

    public void StopAllSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    public void SetVolume()
    {
        int count = 0;

        foreach (Sound s in sounds)
        {
            //s.source.volume = masterSound;

            //if (s.source.volume < 1)
            //    Debug.Log("Volume: " + s.source.volume);

            if (count < 4) // BGM
                s.source.volume = masterSound - (1f - bgmSound);
            else // SFX
                s.source.volume = masterSound - (1f - sfxSound);

            if (count < 1)
                Debug.Log("Volume: " + s.source.volume);

            count++;
        }
    }


}
