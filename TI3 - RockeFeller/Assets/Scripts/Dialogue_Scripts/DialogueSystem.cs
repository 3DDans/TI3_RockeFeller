using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Choices")]
    public GameObject choicePanel;
    public Button[] choiceButtons;
    public TextMeshProUGUI[] choiceTexts;

    [Header("Config")]
    public float typingSpeed = 0.03f;

    private Dialogue currentDialogue;
    private int index;
    private bool isTyping;

    public System.Action onDialogueEnd;

    // =======================

    public void StartDialogue(Dialogue data)
    {
        currentDialogue = data;
        index = 0;
        choicePanel.SetActive(false);

        dialoguePanel.SetActive(true);
        ShowLine();
    }

    void Update()
    {
        if (!dialoguePanel.activeSelf) return;

    
        if (choicePanel.activeSelf) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = currentDialogue.lines[index].text;
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    void ShowLine()
    {
        StopAllCoroutines();
        choicePanel.SetActive(false);

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        string text = currentDialogue.lines[index].text;

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        if (currentDialogue.lines[index].hasChoices)
        {
            ShowChoices();
        }

    }

    void NextLine()
    {
        
        if (currentDialogue.lines[index].hasChoices)
            return;

        index++;

        if (index >= currentDialogue.lines.Length || currentDialogue.lines[index - 1].isEnd)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    void ShowChoices()
    {
        choicePanel.SetActive(true);

        var choices = currentDialogue.lines[index].choices;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceTexts[i].text = choices[i].text;

                int choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => Choose(choiceIndex));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void Choose(int choiceIndex)
    {
        choicePanel.SetActive(false);

        index = currentDialogue.lines[index].choices[choiceIndex].nextIndex;
        ShowLine();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);

   
        onDialogueEnd?.Invoke();
    }
}