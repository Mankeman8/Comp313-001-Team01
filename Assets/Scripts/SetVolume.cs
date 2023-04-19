using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer effectMixer;
    public Slider musicSlider;
    public Slider effectSlider;
    public string musicExposedParam;
    public string effectExposedParam;

    public void SetMusicLevel()
    {
        //Step 1: Expose the volume parameter on the mixer.
        //Step 2: rename it to whatever you want
        //Step 3: set the float of the mixer to the value of the slider
        musicMixer.SetFloat(musicExposedParam, musicSlider.value);
        musicSlider = GameObject.Find("Music Slider").GetComponent<Slider>();
        //If it goes to the min value, silence it since the person wants it to be 0
        if(musicSlider.value == -50)
        {
            musicMixer.SetFloat(musicExposedParam, -100);
        }
    }
    public void SetSFXLevel()
    {
        //Read the above comments
        effectMixer.SetFloat(effectExposedParam, effectSlider.value);
        effectSlider = GameObject.Find("SFX Slider").GetComponent<Slider>();
        if (musicSlider.value == -50)
        {
            effectMixer.SetFloat(effectExposedParam, -100);
        }
    }
}
