using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CutsceneDialogueGiver : MonoBehaviour
{
    [Header("Dialogue")]
    public Dialogue dialogue;
    public DialogueSystemCutscene dialogueSystem;

    public void StartDialogue()
    {
        CursorManager.Instance.ShowCursor();;

        dialogueSystem.StartDialogue(dialogue);
    }
}
