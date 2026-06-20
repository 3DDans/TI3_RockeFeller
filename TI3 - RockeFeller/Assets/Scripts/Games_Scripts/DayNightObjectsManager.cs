using System.Collections.Generic;
using UnityEngine;

public class DayNightObjectsManager : MonoBehaviour
{
    public static DayNightObjectsManager Instance;

    [Header("Visible Only During Day")]
    public List<GameObject> dayOnlyObjects = new();

    [Header("Visible Only During Night")]
    public List<GameObject> nightOnlyObjects = new();

    private void Awake()
    {
        Instance = this;
    }

    public void SetDay()
    {
        foreach (GameObject obj in dayOnlyObjects)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        foreach (GameObject obj in nightOnlyObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    public void SetNight()
    {
        foreach (GameObject obj in dayOnlyObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        foreach (GameObject obj in nightOnlyObjects)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
}