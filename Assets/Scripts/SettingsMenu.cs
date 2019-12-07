using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

    public AudioMixer MainMixer;
    public Slider Slider;
    private float sliderValue;

    private void Start()
    {
        MainMixer = AudioManager.instance.GameGroup.audioMixer;
        sliderValue = PlayerPrefs.GetFloat("GameVolume");
        Slider.value = sliderValue;
        //MainMixer.GetFloat("Volume", out sliderValue);
        //Slider.value = sliderValue;
    }

    public void SetVolume(float amount)
    {
        Debug.Log("Volume " + amount);
        MainMixer.SetFloat("Volume", amount);
        PlayerPrefs.SetFloat("GameVolume", amount);
    }
}
