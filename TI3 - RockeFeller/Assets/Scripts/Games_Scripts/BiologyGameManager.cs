using UnityEngine;
using TMPro;

public class BiologyGameManager : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;
    public GameObject gameUI;

    public NPCInteraction npc;

    public void CorrectChoice()
    {
        feedbackText.text = "Organismo identificado!";
        Invoke(nameof(EndGame), 1.5f);
    }

    public void WrongChoice()
    {
        feedbackText.text = "Não parece ser esse...";
    }

    void EndGame()
    {
        gameUI.SetActive(false);
        npc.CompletePuzzle();
    }
}