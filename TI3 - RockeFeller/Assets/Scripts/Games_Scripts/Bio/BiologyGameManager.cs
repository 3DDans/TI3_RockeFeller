using UnityEngine;
using TMPro;

public class BiologyGameManager : MinigameBase
{
    public TextMeshProUGUI feedbackText;
    public GameObject gameUI;

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

        CompleteMinigame();
    }
}