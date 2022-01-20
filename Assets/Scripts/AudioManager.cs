using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    //public AudioMixer audioMixer;

    public static AudioManager instance; //using instance so there is not more than 1 Audiomanager

    

    void Awake() {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject); //if there is already an audio manager, destroy and return to not run any more code
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("BackgroundMusic");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //Checks Sounds[] for sound where sound.name = name
        if(s == null)//if name not found in array
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
            
        s.source.Play();
    }

    public void StopPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //Checks Sounds[] for sound where sound.name = name
        if(s == null)//if name not found in array
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
            
        s.source.Stop();
    }

    public void SetMainVolume(float volume)
    {
        foreach(Sound s in sounds)
        {
            s.audioMixer.SetFloat("volume",volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        foreach(Sound s in sounds)
        {
            s.audioMixer.SetFloat("volume",volume);
        }
    }
}
