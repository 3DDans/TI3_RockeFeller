using UnityEngine;

public class MinigameBase : MonoBehaviour
{
    [Header("Minigame")]
    public MinigameID minigameID;

    [Header("NPC")]
    public NPCInteraction npc;

    public virtual void CompleteMinigame()
    {
        GameProgressManager.Instance.CompleteMinigame(minigameID);

        if (npc != null)
        {
            npc.CompletePuzzle();
        }
    }
}