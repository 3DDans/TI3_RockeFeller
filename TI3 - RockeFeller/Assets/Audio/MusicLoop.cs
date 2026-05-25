using UnityEngine;

public class MusicLoop : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicClip;

    void Start()
    {
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }
}