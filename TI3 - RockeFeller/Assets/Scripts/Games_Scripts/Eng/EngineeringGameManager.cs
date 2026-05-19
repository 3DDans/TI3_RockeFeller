using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EngineeringGameManager : MinigameBase
{
    private BuildPart selectedPart; 
    public TextMeshProUGUI feedbackText;
    public List<BuildSlot> allSlots;
    public BuildPart[] allParts;

    public bool IsAssemblyComplete()
    {
        return allParts.All(p => p.isPlaced);
    }

    // ?? Quando o player clica numa peça
    public void SelectPart(BuildPart part)
    {
        if (selectedPart != null)
        {
            var oldFloat = selectedPart.GetComponent<PartJiggle>();
            if (oldFloat != null)
                oldFloat.StopFloat();
        }

        selectedPart = part;

        var floatEffect = part.GetComponent<PartJiggle>();
        if (floatEffect != null)
            floatEffect.StartFloat();

        UpdateHighlights();
    }

    // ?? Quando clicar em slot (vamos usar depois)
    public void TryPlace(BuildSlot slot)
    {
        if (selectedPart == null) return;

        if (slot.acceptedType == selectedPart.type && !slot.occupied)
        {
            selectedPart.Place(slot);
            selectedPart = null;

            Debug.Log("Encaixou!");
            ClearHighlights();
        }
        else
        {
            Debug.Log("Não encaixa!");
        }
    }
    void UpdateHighlights()
    {
        Debug.Log("Atualizando highlights");

        foreach (var slot in allSlots)
        {
            bool valid = slot.acceptedType == selectedPart.type && !slot.occupied;

            Debug.Log(slot.name + " válido: " + valid);

            slot.Highlight(valid);
        }
    }

    public void ClearHighlights()
    {
        foreach (var slot in allSlots)
        {
            slot.Highlight(false);
        }
    }
}

