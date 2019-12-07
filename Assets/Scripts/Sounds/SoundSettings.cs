using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]         // см Script Serialization
public class SoundSettings
{
    public string Name;

    public AudioClip Clip;

    //[Range(0f, 1f)]
    //public float Volume;
    //[Range(.1f, 3f)]
    //public float Pitch;
    public bool Loop;

    [HideInInspector]
    public AudioMixerGroup Group;

    [HideInInspector]
    public AudioSource Source;
}
