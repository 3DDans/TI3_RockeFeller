using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("UI")]
    public GameObject interactionUI;

    [Header("Cameras")]
    public CinemachineCamera mainCamera;
    public CinemachineCamera dialogueCamera;
    public CinemachineCamera minigameCamera;

    [Header("Dialogue")]
    public List<DialogueEntry> dialogues = new();
    public DialogueSystem dialogueSystem;


    [Header("Puzzle")]
    public GameObject puzzleUI;
    public GameObject playerVs;
    public MinigameTabletManager tabletManager;

    [Header("Type")]
    public NPCRole npcRole;

    [Header("Quest Link")]
    public NPCInteraction linkedMinigameNPC;
    public MinigameID minigameID;
     
    private bool playerInRange = false;
    private bool hasTalked = false;
    private ThirdPersonMovement player;
 




    void Start()
    {
        player = FindFirstObjectByType<ThirdPersonMovement>();

        if (npcRole == NPCRole.Minigame)
        {
            interactionUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        switch (npcRole)
        {
            case NPCRole.Ambient:
            case NPCRole.BuildingHint:
            case NPCRole.QuestGiver:

                StartDialogue();
                break;

            case NPCRole.Minigame:

                if (!GameProgressManager.Instance.IsUnlocked(minigameID))
                    return;

                StartMinigame();
                break;
        }
    }

    // ================= DIALOGUE =================

    void StartDialogue()
    {
        GameProgressManager.IsInMinigame = true;
        player.canMove = false;
        playerVs.SetActive(false);

        dialogueCamera.Priority = 20;
        mainCamera.Priority = 10;
        interactionUI.SetActive(false);

        CursorManager.Instance.ShowCursor();

        Dialogue dialogueToUse = GetCurrentDialogue();

        dialogueSystem.StartDialogue(dialogueToUse);
        dialogueSystem.onDialogueEnd = OnDialogueEnd;

    }

    Dialogue GetCurrentDialogue()
    {
        DialogueStage currentStage = GetCurrentDialogueStage();

        foreach (var entry in dialogues)
        {
            if (entry.stage == currentStage)
            {
                return entry.dialogue;
            }
        }

        return null;
    }
    DialogueStage GetCurrentDialogueStage()
    {
        GameProgressManager progress = GameProgressManager.Instance;

        // Pós jogo
        if (progress.gameFinished)
        {
            return DialogueStage.Finished;
        }

        // Final do jogo
        if (progress.finalStageUnlocked)
        {
            return DialogueStage.SecondPhase;
        }

        // Segunda fase
        if (progress.meteorUnlocked)
        {
            return DialogueStage.MeteorUnlocked;
        }

        // Pós minigame
        if (
            minigameID != MinigameID.None &&
            progress.IsCompleted(minigameID)
        )
        {
            return DialogueStage.AfterPuzzle;
        }

        // Primeira conversa
        if (!hasTalked)
        {
            return DialogueStage.FirstTime;
        }

        // Conversas repetidas
        return DialogueStage.BeforePuzzle;
    }

    void OnDialogueEnd()
    {
        GameProgressManager.IsInMinigame = false;
        hasTalked = true;
        if (npcRole == NPCRole.QuestGiver)
        {
            UnlockMinigameNPC();
        }
        interactionUI.SetActive(true);
        playerVs.SetActive(true);
        EndInteraction();
    }

    void UnlockMinigameNPC()
    {
        GameProgressManager.Instance.UnlockMinigame(minigameID);

        if (linkedMinigameNPC != null)
        {
            linkedMinigameNPC.EnableInteraction();
        }
    }
    // ================= MINIGAME =================

    void StartMinigame()
    {
        if (minigameID != MinigameID.Biologia)
        {
            CursorManager.Instance.ShowCursor();
        }

        if (tabletManager != null)
        {
            tabletManager.EnableTablet();
        }
        GameProgressManager.IsInMinigame = true;
        player.canMove = false;
        playerVs.SetActive(false);

        minigameCamera.Priority = 20;
        mainCamera.Priority = 10;
        interactionUI.SetActive(false);

       

        if (puzzleUI != null)
            puzzleUI.SetActive(true);
    }
    public void CompletePuzzle()
    {
       

        GameProgressManager.Instance.CompleteMinigame(minigameID);

        EndMinigame();
    }

    void EndMinigame()
    {
        GameProgressManager.IsInMinigame = false;
        if (puzzleUI != null)
            puzzleUI.SetActive(false);
        
        EndInteraction();
        if (tabletManager != null)
        {
            tabletManager.DisableTablet();
        }
    }

    // ================= GERAL =================    

    void EndInteraction()
    {
        player.canMove = true;

        dialogueCamera.Priority = 5;
        minigameCamera.Priority = 5;
        mainCamera.Priority = 10;

        playerVs.SetActive(true);

        CursorManager.Instance.HideCursor();
    }

    void EnableInteraction()
    {
        if(playerInRange && GameProgressManager.Instance.IsUnlocked(minigameID))
        {
        interactionUI.SetActive(true);
        }
           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        switch (npcRole)
        {
            case NPCRole.Ambient:
            case NPCRole.BuildingHint:
            case NPCRole.QuestGiver:

                interactionUI.SetActive(true);
                break;

            case NPCRole.Minigame:

                if (GameProgressManager.Instance.IsUnlocked(minigameID))
                {
                    interactionUI.SetActive(true);
                }

                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        interactionUI.SetActive(false);
    }
}


public enum NPCRole
{
    Ambient,
    BuildingHint,
    QuestGiver,
    Minigame
}