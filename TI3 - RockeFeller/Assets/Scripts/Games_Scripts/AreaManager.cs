using UnityEngine;
using static ExaminationArea;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance;

    public TaskArea CurrentArea;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public string GetAreaName(TaskArea area)
    {
        switch (area)
        {
            case TaskArea.Biologia:
                return "BIOLOGY DEPARTMENT";

            case TaskArea.Medicina:
                return "MEDICINE DEPARTMENT";

            case TaskArea.Engenharia:
                return "TECHNOLOGY DEPARTMENT";

            case TaskArea.Campus:
                return "CAMPUS PUNKOFELLER";

            default:
                return "";
        }
    }

    public void SetArea(TaskArea newArea)
    {
        CurrentArea = newArea;

        AmbientManager.Instance.SetArea(newArea);
    }

}

