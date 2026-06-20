using System.Collections.Generic;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    public static AmbientManager Instance;

    [System.Serializable]
    public class AreaAmbient
    {
        public TaskArea area;
        public AudioClip clip;
    }

    [Header("Ambientes por área")]
    public List<AreaAmbient> ambientList = new List<AreaAmbient>();

    private Dictionary<TaskArea, AudioClip> ambientDict;

    private AudioSource audioSource;

    private TaskArea currentArea;

    void Awake()
    {
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

        ambientDict = new Dictionary<TaskArea, AudioClip>();

        foreach (var ambient in ambientList)
        {
            if (!ambientDict.ContainsKey(ambient.area))
                ambientDict.Add(ambient.area, ambient.clip);
        }
    }

    public void SetArea(TaskArea newArea)
    {
        if (currentArea == newArea) return;

        currentArea = newArea;

        if (ambientDict.TryGetValue(newArea, out AudioClip clip))
        {
            StartCoroutine(SwitchAmbient(clip));
        }
        else
        {
            Debug.LogWarning("Sem ambient para área: " + newArea);
        }
    }

    private System.Collections.IEnumerator SwitchAmbient(AudioClip newClip)
    {
        // fade out simples
        float startVolume = audioSource.volume;

        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / 0.5f);
            yield return null;
        }

        audioSource.clip = newClip;
        audioSource.loop = true;
        audioSource.Play();

        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, t / 0.5f);
            yield return null;
        }

        audioSource.volume = startVolume;
    }
}