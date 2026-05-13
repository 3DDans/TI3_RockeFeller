using UnityEngine;

public class BuildPart : MonoBehaviour
{
    public PartType type;
    public bool isPlaced = false;

    public void Place(BuildSlot slot)
    {
        isPlaced = true;
        slot.occupied = true;

        slot.Highlight(false);

        if (slot.installedVisual != null)
            slot.installedVisual.SetActive(true);

        gameObject.SetActive(false);
    }
}