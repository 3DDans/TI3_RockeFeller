using UnityEngine;
using TMPro;

public class BiologyGameManager : MinigameBase
{
    public TextMeshProUGUI feedbackText;
    public GameObject gameUI;
    public static bool gameStarted = false;
    public Transform vfxPoint;

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
        AnalyticsManager.Instance.MarcarPuzzleFinished(AnalyticsManager.Area.Biology);
        gameUI.SetActive(false);
        VFXManager.Instance.PlayVFX("Correct", vfxPoint.position);
        SoundFXManager.Instance.PlaySFX("Correct");
        CompleteMinigame();
    }
}