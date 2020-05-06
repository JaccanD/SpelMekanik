using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
// Author Jack fö fan
public class SetVolume : MonoBehaviour
{
    public AudioMixer Mixer;
    public Slider MusicSlider;
    public Slider SFXSlider;

    public void Start()
    {
        MusicSlider.value = Mathf.Pow(10, Volume.MusicVolume / 20);
        SFXSlider.value = Mathf.Pow(10, Volume.SFXVolume / 20);
        Mixer.SetFloat("MusicVol", Volume.MusicVolume);
        Mixer.SetFloat("SFXVol", Volume.SFXVolume);
    }
    public void SetLevelMusic (float sliderValue)
    {
        Volume.MusicVolume = Mathf.Log10(sliderValue) * 20;
        Mixer.SetFloat("MusicVol", Volume.MusicVolume);
    }

    public void SetLevelSFX (float sliderValue)
    {
        Volume.SFXVolume = Mathf.Log10(sliderValue) * 20;
        Mixer.SetFloat("SFXVol", Volume.SFXVolume);
    }

}
