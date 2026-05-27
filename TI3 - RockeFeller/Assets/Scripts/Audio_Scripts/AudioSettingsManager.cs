using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    [Header("Mixer")]
    public AudioMixer mixer;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Carrega valores salvos
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMasterVolume()
    {
        float volume = Mathf.Log10(masterSlider.value) * 20;

        mixer.SetFloat("MasterVolume", volume);

        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }

    public void SetMusicVolume()
    {
        float volume = Mathf.Log10(musicSlider.value) * 20;

        mixer.SetFloat("MusicVolume", volume);

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        float volume = Mathf.Log10(sfxSlider.value) * 20;

        mixer.SetFloat("SFXVolume", volume);

        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
}