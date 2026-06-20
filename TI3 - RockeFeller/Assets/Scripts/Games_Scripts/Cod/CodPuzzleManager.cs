using UnityEngine;

public class CodPuzzleManager : MinigameBase
{
    public GameObject[] puzzles;
    public int currentPuzzle;
    public NPCInteraction npcInteraction;

    private void Start()
    {
        currentPuzzle = 0;
        for (int i = 0; i < puzzles.Length; i++)
        {
            if (i == currentPuzzle) puzzles[i].gameObject.SetActive(true);
            else puzzles[i].gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    public void TerminouNivel()
    {
        puzzles[currentPuzzle].gameObject.SetActive(false);

        Debug.Log("Nivel Finalizado");
        currentPuzzle++;

        if (currentPuzzle < puzzles.Length)
            puzzles[currentPuzzle].gameObject.SetActive(true);

        if (currentPuzzle == puzzles.Length)
        {
            AnalyticsManager.Instance.MarcarPuzzleFinished(AnalyticsManager.Area.Programming);
            CompleteMinigame();
        }
    }
}
