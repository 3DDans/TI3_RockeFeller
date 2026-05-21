using UnityEngine;

public class MedicalToolManager : MonoBehaviour
{
    public static MedicalToolManager Instance;

    [Header("Camera")]
    public Camera medicalCamera;

    [Header("Current Tool")]
    public MedicalToolType currentTool = MedicalToolType.None;

    [Header("Held Tool Visual")]
    public Transform heldToolTransform;

    private GameObject currentHeldObject;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        FollowMouse();
    }

    void FollowMouse()
    {
        if (currentHeldObject == null)
            return;

        Ray ray = medicalCamera.ScreenPointToRay(Input.mousePosition);

        Vector3 targetPos = ray.GetPoint(2f);

        heldToolTransform.position = targetPos;
    }

    public void EquipTool(MedicalToolType toolType, GameObject toolPrefab)
    {
        currentTool = toolType;

        if (currentHeldObject != null)
        {
            Destroy(currentHeldObject);
        }

        currentHeldObject = Instantiate(
            toolPrefab,
            heldToolTransform.position,
            Quaternion.identity,
            heldToolTransform
        );
    }

    public bool HasTool()
    {
        return currentTool != MedicalToolType.None;
    }
}