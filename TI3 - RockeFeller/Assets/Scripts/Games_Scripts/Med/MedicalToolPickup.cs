using UnityEngine;

public class MedicalToolPickup : MonoBehaviour
{
    public MedicalToolType toolType;

    public GameObject tableObject;

    void OnMouseDown()
    {
        if (!MedicalGameManager.IsPlayingMedicalGame)
            return;

        MedicalToolManager.Instance
            .EquipTool(toolType, tableObject);
    }
}