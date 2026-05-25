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
    public Transform heldToolPoint;

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
        Vector3 mousePos = Input.mousePosition;

        float x = (mousePos.x / Screen.width - 0.5f) * 4f;
        float y = (mousePos.y / Screen.height - 0.5f) * 2f;

        heldToolPoint.localPosition =
            new Vector3(x, y, 0.7f);
    }

    public void EquipTool(MedicalToolType tool, GameObject tableObject)
    {
        // devolve ferramenta antiga pra mesa
        if (currentTableObject != null)
        {
            currentTableObject.SetActive(true);
        }

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

        // esconde a nova ferramenta da mesa
        if (currentTableObject != null)
        {
            currentTableObject.SetActive(false);
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