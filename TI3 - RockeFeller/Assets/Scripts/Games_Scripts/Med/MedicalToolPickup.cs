using UnityEngine;

public class MedicalToolPickup : MonoBehaviour
{
    public MedicalToolType toolType;

    public GameObject tableObject;

    void OnMouseDown()
    {
        MedicalToolManager.Instance.EquipTool(toolType, tableObject);

        tableObject.SetActive(false);
    }
}