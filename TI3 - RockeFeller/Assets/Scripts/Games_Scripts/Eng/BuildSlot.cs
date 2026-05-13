using UnityEngine;

public class BuildSlot : MonoBehaviour
{
    public PartType acceptedType;
    public bool occupied = false;

    public GameObject visual;

    public GameObject installedVisual;


    void Start()
    {
        if (visual != null)
            visual.SetActive(false);
    }

    public void Highlight(bool state)
    {
        if (visual != null) { }
            visual.SetActive(state);
    }
    public void Place(BuildSlot slot)
    {
        
        slot.occupied = true;

        if (slot.installedVisual != null)
            slot.installedVisual.SetActive(true);

        gameObject.SetActive(false);
    }       
}