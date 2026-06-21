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

    [System.Serializable]
    public class AreaMusic
    {
        public TaskArea area;

        [Header("Dia")]
        public AudioClip dayClip;

        [Header("Noite")]
        public AudioClip nightClip;
    }

    [Header("Lista de Musicas")]
    public List<Music> musicList = new List<Music>();

    [Header("Musicas por Area")]
    public List<AreaMusic> areaMusics = new List<AreaMusic>();

    private Dictionary<string, AudioClip> musicDictionary;

    private AudioSource musicSource;

    private TaskArea currentArea;

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

    // =========================================
    // Sistema antigo (mantido)
    // =========================================

    public void PlayMusic(string musicName)
    {
        if (musicDictionary.TryGetValue(musicName, out AudioClip clip))
        {
            if (musicSource.clip == clip)
                return;

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

    // =========================================
    // Sistema novo de Areas
    // =========================================

    public void SetArea(TaskArea newArea)
    {
        currentArea = newArea;
        RefreshMusic();
    }

    public void RefreshMusic()
    {
        foreach (AreaMusic music in areaMusics)
        {
            if (music.area != currentArea)
                continue;

            AudioClip clipToPlay = music.dayClip;

            // Som noturno apenas quando for noite
            if (SkyboxManager.Instance != null &&
                SkyboxManager.Instance.isNight &&
                music.nightClip != null)
            {
                clipToPlay = music.nightClip;
            }

            if (musicSource.clip == clipToPlay)
                return;

            musicSource.clip = clipToPlay;
            musicSource.loop = true;
            musicSource.Play();

            return;
        }
    }
}