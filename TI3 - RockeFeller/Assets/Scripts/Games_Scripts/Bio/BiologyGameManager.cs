using UnityEngine;
using TMPro;

public class BiologyGameManager : MinigameBase
{
    public TextMeshProUGUI feedbackText;
    public GameObject gameUI;
    public static bool gameStarted = false;

    public void CorrectChoice() 
    {
        feedbackText.text = "Organism identified!";

        Invoke(nameof(EndGame), 1.5f);
    }

    public void WrongChoice()
    {
        feedbackText.text = "It doesn't seem to be that one...";
    }

    void EndGame()
    {
        gameUI.SetActive(false);

        CompleteMinigame();
    }
}