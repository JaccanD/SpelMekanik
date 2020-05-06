using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
// Author Jack fö fan
public class SetVolume : MonoBehaviour
{
    public AudioMixer Mixer;

    public void SetLevelMusic (float sliderValue)
    {
        Mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelSFX (float sliderValue)
    {
        Mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }

}
