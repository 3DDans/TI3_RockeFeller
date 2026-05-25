using UnityEngine;

public class BuildPart : MonoBehaviour
{
    [Header("Part Info")]
    public PartType type;

    [Header("States")]
    public bool isBroken = false;
    public bool isRepaired = false;
    public bool isPlaced = false;

    [Header("Visuals")]
    public GameObject normalVisual;
    public GameObject brokenVisual;

    void Start()
    {
        UpdateVisualState();
    }

    public bool CanBePlaced()
    {
        // Só pode encaixar se:
        // NÃO estiver quebrada
        // OU estiver reparada

        return !isBroken || isRepaired;
    }

    public void Repair()
    {
        isRepaired = true;

        UpdateVisualState();
    }

    void UpdateVisualState()
    {
        bool showBroken =
            isBroken && !isRepaired;

        if (normalVisual != null)
            normalVisual.SetActive(!showBroken);

        if (brokenVisual != null)
            brokenVisual.SetActive(showBroken);
    }

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