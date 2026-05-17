using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("UI Sliders")]
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Configurações")]
    [SerializeField] private float defaultVolume = 0.7f;

    void Start()
    {
        SetupSliders();

        LoadVolumes();

        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    void SetupSliders()
    {
        musicVolumeSlider.minValue = 0f;
        musicVolumeSlider.maxValue = 1f;

        sfxVolumeSlider.minValue = 0f;
        sfxVolumeSlider.maxValue = 1f;
    }

    void LoadVolumes()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", defaultVolume);
        musicVolumeSlider.value = savedMusicVolume;
        OnMusicVolumeChanged(savedMusicVolume);

        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", defaultVolume);
        sfxVolumeSlider.value = savedSFXVolume;
        OnSFXVolumeChanged(savedSFXVolume);
    }

    public void OnMusicVolumeChanged(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save();
        }
    }

    public void OnSFXVolumeChanged(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
            PlayerPrefs.SetFloat("SFXVolume", volume);
            PlayerPrefs.Save();

        }
    }

    // Método para resetar volumes ao padrão
    public void ResetToDefault()
    {
        musicVolumeSlider.value = defaultVolume;
        sfxVolumeSlider.value = defaultVolume;
    }
}