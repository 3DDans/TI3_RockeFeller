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
                return "ENGINEERING DEPARTMENT";

            case TaskArea.Campus:
                return "CAMPUS PUNKOFELLER";

            default:
                return "";
        }
    }

}

