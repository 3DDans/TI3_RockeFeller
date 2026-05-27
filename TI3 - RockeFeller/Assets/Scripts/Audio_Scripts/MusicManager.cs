using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [System.Serializable]
    public class Music
    {
        public string name;
        public AudioClip clip;
    }

    [Header("Lista de Musicas")]
    public List<Music> musicList = new List<Music>();

    private Dictionary<string, AudioClip> musicDictionary;

    private AudioSource musicSource;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource = GetComponent<AudioSource>();

        musicDictionary = new Dictionary<string, AudioClip>();

        foreach (Music music in musicList)
        {
            if (!musicDictionary.ContainsKey(music.name))
            {
                musicDictionary.Add(music.name, music.clip);
            }
        }
    }

    public void PlayMusic(string musicName)
    {
        if (musicDictionary.TryGetValue(musicName, out AudioClip clip))
        {
            if (musicSource.clip == clip) return;

            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Musica não encontrada: " + musicName);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}