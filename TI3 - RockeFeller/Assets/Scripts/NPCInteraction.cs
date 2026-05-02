using UnityEngine;
using Unity.Cinemachine;

public class NPCInteraction : MonoBehaviour
{
    [Header("UI")]
    public GameObject interactionUI;

    [Header("Cameras")]
    public CinemachineCamera mainCamera;
    public CinemachineCamera dialogueCamera;
    public CinemachineCamera minigameCamera;

    [Header("Dialogue")]
    public DialogueSystem dialogueSystem;
    public Dialogue firstDialogue;
    public Dialogue beforePuzzleDialogue;
    public Dialogue afterPuzzleDialogue;
   

    [Header("Puzzle")]
    public GameObject puzzleUI;
    public GameObject playerVs;

   [Header("Type")]
    public NPCType npcType;

    [Header("Quest Link")]
    public NPCInteraction linkedMinigameNPC;

    private bool playerInRange = false;
    private ThirdPersonMovement player;

    private NPCState currentState = NPCState.FirstTime;

    private bool questUnlocked = false;
    private bool puzzleCompleted = false;

    void Start()
    {
        player = FindFirstObjectByType<ThirdPersonMovement>();

        if (npcType == NPCType.Minigame)
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
        if (npcType == NPCType.QuestGiver)
        {
            StartDialogue();
        }
        else if (npcType == NPCType.Minigame)
        {
            if (!questUnlocked) return;

            StartMinigame();
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
        switch (currentState)
        {
            case NPCState.FirstTime:
                return firstDialogue;

            case NPCState.BeforePuzzle:
                return beforePuzzleDialogue;

            case NPCState.AfterPuzzle:
                return afterPuzzleDialogue;
        }

        return firstDialogue;
    }

    void OnDialogueEnd()
    {
        if (npcType == NPCType.QuestGiver)
        {
            if (currentState == NPCState.FirstTime)
            {
                currentState = NPCState.BeforePuzzle;
                UnlockMinigameNPC();
                EndInteraction();
                return;
            }

            if (currentState == NPCState.BeforePuzzle)
            {
                EndInteraction();
                return;
            }

            if (currentState == NPCState.AfterPuzzle)
            {
                EndInteraction();
                return;
            }
        }
    }

    void UnlockMinigameNPC()
    {
        if (linkedMinigameNPC != null)
        {
            linkedMinigameNPC.questUnlocked = true;
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

        // Atualiza estado do NPC que deu a missão
        if (linkedMinigameNPC != null)
        {
            linkedMinigameNPC.currentState = NPCState.AfterPuzzle;
        }

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
        if (playerInRange)
            interactionUI.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (npcType == NPCType.QuestGiver)
        {
            interactionUI.SetActive(true);
        }
        else if (npcType == NPCType.Minigame && questUnlocked)
        {
            interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        interactionUI.SetActive(false);
    }
}

public enum NPCState
{
    FirstTime,
    BeforePuzzle,
    AfterPuzzle
}

public enum NPCType
{
    QuestGiver,
    Minigame
}