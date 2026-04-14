using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea(2, 5)]
    public string text;

    [Header("Choices")]
    public bool hasChoices;
    public Choice[] choices;

    [Header("End")]
    public bool isEnd;
}