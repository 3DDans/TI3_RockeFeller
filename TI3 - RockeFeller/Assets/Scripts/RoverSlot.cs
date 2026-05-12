using UnityEngine;

public class RoverSlot : MonoBehaviour
{
    public PartType acceptedType;
    public bool occupied = false;

    public GameObject visual;

    void Start()
    {
        if (visual != null)
            visual.SetActive(false);
    }

    public void Highlight(bool state)
    {
        if (visual != null) { }
           // visual.SetActive(state);
    }
}