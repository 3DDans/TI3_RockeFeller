using System.Collections.Generic;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    public static AmbientManager Instance;

    [System.Serializable]
    public class AreaAmbient
    {
        public TaskArea area;

        [Header("Dia")]
        public AudioClip dayClip;

        [Header("Noite")]
        public AudioClip nightClip;
    }

    [Header("Ambientes por área")]
    public List<AreaAmbient> ambientList = new List<AreaAmbient>();



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

       
    }

    public void SetArea(TaskArea newArea)
    {
        if (currentArea == newArea)
            return;

        currentArea = newArea;

        RefreshAmbient();
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

    public void RefreshAmbient()
    {
        foreach (var ambient in ambientList)
        {
            if (ambient.area != currentArea)
                continue;

            AudioClip clipToPlay = ambient.dayClip;

            // Campus usa som diferente à noite
            if (currentArea == TaskArea.Campus &&
                DayNightObjectsManager.Instance != null &&
                SkyboxManager.Instance.isNight)
            {
                clipToPlay = ambient.nightClip;
            }

            if (audioSource.clip != clipToPlay)
            {
                StartCoroutine(SwitchAmbient(clipToPlay));
            }

            return;
        }
    }


}