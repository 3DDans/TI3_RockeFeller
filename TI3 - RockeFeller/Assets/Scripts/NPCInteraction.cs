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

    [Header("Type")]
    public NPCRole npcRole;

    [Header("Quest Link")]
    public NPCInteraction linkedMinigameNPC;
    public MinigameID minigameID;

    private bool playerInRange = false;
    private bool hasTalked = false;
    private ThirdPersonMovement player;



    private bool questUnlocked = false;
    private bool puzzleCompleted = false;

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
        player.canMove = false;

        dialogueCamera.Priority = 20;
        mainCamera.Priority = 10;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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
        hasTalked = true;
        if (npcRole == NPCRole.QuestGiver)
        {
            UnlockMinigameNPC();
        }
        
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
        player.canMove = false;
        playerVs.SetActive(false);

        minigameCamera.Priority = 20;
        mainCamera.Priority = 10;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (puzzleUI != null)
            puzzleUI.SetActive(true);
    }
    public void CompletePuzzle()
    {
        puzzleCompleted = true;

        GameProgressManager.Instance.CompleteMinigame(minigameID);

        EndMinigame();
    }

    void EndMinigame()
    {
        if (puzzleUI != null)
            puzzleUI.SetActive(false);

        EndInteraction();
    }

    // ================= GERAL =================

    void EndInteraction()
    {
        player.canMove = true;

        dialogueCamera.Priority = 5;
        minigameCamera.Priority = 5;
        mainCamera.Priority = 10;

        playerVs.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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