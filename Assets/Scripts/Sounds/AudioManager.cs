using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    public SoundSettings[] Sounds;
    public AudioMixerGroup GameGroup;
    private AudioMixer Mixer;
    private float temp;

    void Awake()
    { 
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (var sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;

            sound.Group = GameGroup;
            sound.Source.outputAudioMixerGroup = sound.Group;
            sound.Source.loop = sound.Loop;
        }
    }

    private void Start()
    {
        Play("MainTheme");

        var gameVolume = PlayerPrefs.GetFloat("GameVolume");       // устанавливаем громкость всей игры
        GameGroup.audioMixer.SetFloat("Volume", gameVolume);
    }

    public void Play(string name)
    {
   //     var soundToPlay = Array.Find(Sounds, s => s.Name == name) ?? throw new ArgumentException(name);
       var soundToPlay = Array.Find(Sounds, s => s.Name == name);

        if(soundToPlay == null)
        {
            Debug.LogWarning("Sound of Name " + name + " wasn't found");
            return;
        }

        soundToPlay.Source.Play();
    }

    public void Play(int index)
    {
        var soundToPlay = Sounds[index];
        soundToPlay.Source.Play();
    }
}
