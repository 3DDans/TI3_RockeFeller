using UnityEngine;

public class CodPuzzleManager : MinigameBase
{
    public GameObject[] puzzles;
    public int currentPuzzle;
    public int niveisFinalizados = 0;
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
        Debug.Log("Nivel Finalizado");
        niveisFinalizados++;

        if(niveisFinalizados == puzzles.Length)
        {
            CompleteMinigame();
        }
    }
}
