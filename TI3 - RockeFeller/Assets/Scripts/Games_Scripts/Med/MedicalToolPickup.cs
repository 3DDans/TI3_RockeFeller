using UnityEngine;

public class MedicalToolPickup : MonoBehaviour
{
    public MedicalToolType toolType;

    [Header("Held Prefab")]
    public GameObject heldPrefab;

    void OnMouseDown()
    {
        MedicalToolManager.Instance.EquipTool(toolType, heldPrefab);
        Debug.Log(toolType);
    }
}