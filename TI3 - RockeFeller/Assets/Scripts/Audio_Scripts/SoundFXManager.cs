using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    [System.Serializable]
    public class SFX
    {
        public string name;
        public AudioClip clip;
    }

    [Header("Lista de Sound Effects")]
    public List<SFX> sfxList = new List<SFX>();

    private Dictionary<string, AudioClip> sfxDictionary;

    private AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();

        // Cria dicionario
        sfxDictionary = new Dictionary<string, AudioClip>();

        foreach (SFX sfx in sfxList)
        {
            if (!sfxDictionary.ContainsKey(sfx.name))
            {
                sfxDictionary.Add(sfx.name, sfx.clip);
            }
        }
    }

    public void PlaySFX(string soundName)
    {
        if (sfxDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX não encontrado: " + soundName);
        }
    }
}