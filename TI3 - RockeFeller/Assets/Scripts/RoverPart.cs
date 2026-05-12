using UnityEngine;

public class RoverPart : MonoBehaviour
{
    public PartType type;
    public bool isPlaced = false;

    public void Place(RoverSlot slot)
    {
        transform.position = slot.transform.position;
        transform.rotation = slot.transform.rotation;

        isPlaced = true;
        slot.occupied = true;
    }
}