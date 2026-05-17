using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
       
    }

    public void SetMusicVolume()
    {
        mixer.SetFloat("MasterVolume", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        mixer.SetFloat("SfxVolume", sfxSlider.value);
    }
}