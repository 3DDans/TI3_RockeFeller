using UnityEngine;

public class MedicalToolManager : MonoBehaviour
{
    public static MedicalToolManager Instance;

    [Header("Camera")]
    public Camera medicalCamera;

    [Header("Held Tools")]
    public GameObject thermometerHeld;
    public GameObject stethoscopeHeld;
    public GameObject flashlightHeld;

    [Header("Settings")]
    public float fixedZ = 0f;

    [HideInInspector]
    public MedicalToolType currentTool = MedicalToolType.None;

    private GameObject currentToolObject;

    private GameObject currentTableObject;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        FollowMouse();

        if (Input.GetMouseButtonDown(1))
        {
            UnequipTool();
        }
    }

    void FollowMouse()
    {
        if (currentToolObject == null)
            return;

        Ray ray = medicalCamera.ScreenPointToRay(Input.mousePosition);

        Vector3 targetPosition = ray.GetPoint(2f);

        targetPosition.z = fixedZ;

        currentToolObject.transform.position = targetPosition;
    }

    public void EquipTool(MedicalToolType tool, GameObject tableObject)
    {
        DisableAll();

        currentTool = tool;

        currentTableObject = tableObject;

        switch (tool)
        {
            case MedicalToolType.Thermometer:
                currentToolObject = thermometerHeld;
                break;

            case MedicalToolType.Stethoscope:
                currentToolObject = stethoscopeHeld;
                break;

            case MedicalToolType.Flashlight:
                currentToolObject = flashlightHeld;
                break;
        }

        if (currentToolObject != null)
        {
            currentToolObject.SetActive(true);
        }
    }

    public void UnequipTool()
    {
        DisableAll();

        if (currentTableObject != null)
        {
            currentTableObject.SetActive(true);
        }

        currentTool = MedicalToolType.None;

        currentToolObject = null;
        currentTableObject = null;
    }

    void DisableAll()
    {
        thermometerHeld.SetActive(false);
        stethoscopeHeld.SetActive(false);
        flashlightHeld.SetActive(false);
    }

    public bool HasTool()
    {
        return currentTool != MedicalToolType.None;
    }
}