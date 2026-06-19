using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EngineeringGameManager : MinigameBase
{
    private BuildPart selectedPart;

    public TextMeshProUGUI feedbackText;
    public Transform robotBenchSpawnPoint;


    public List<BuildSlot> allSlots;
    public BuildPart[] allParts;

    public GameObject gameUI;

    public bool IsAssemblyComplete()
    {
        return allParts.All(p => p.isPlaced);
    }

    // Quando o player clica numa pe�a
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

    // Quando clicar em slot
    public void TryPlace(BuildSlot slot)
    {
        if (selectedPart == null)
            return;

        if (slot.acceptedType == selectedPart.type &&
            !slot.occupied &&
            selectedPart.CanBePlaced())
        {
            selectedPart.Place(slot);

            selectedPart = null;

            Debug.Log("Encaixou!");

            ClearHighlights();

            // VERIFICA SE TERMINOU O MINIGAME
            if (IsAssemblyComplete())
            {
                FinishGame();
            }
        }
        else
        {
            Debug.Log("N�o encaixa!");
        }
    }

    void FinishGame()
    {
        feedbackText.text = "Fixed robot!";

        Invoke(nameof(EndGame), 1.5f);
    }

    void EndGame()
    {
        gameUI.SetActive(false);
        PetManager.Instance.SpawnPet(robotBenchSpawnPoint);
        CompleteMinigame();
    }

    void UpdateHighlights()
    {
        Debug.Log("Atualizando highlights");

        foreach (var slot in allSlots)
        {
            bool valid = slot.acceptedType == selectedPart.type && !slot.occupied;

            Debug.Log(slot.name + " v�lido: " + valid);

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