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

   
}

