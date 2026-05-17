using UnityEngine;
using UnityEngine.UI;

public class CurrentPuzzleManager : MonoBehaviour
{
    public DragDrop[] commands;
    public CodPuzzleManager manager;
    int acertos = 0;

    private void Start()
    {
        acertos = 0;
    }

    public void VerifyPuzzle()
    {
        foreach(var command in commands)
        {
            if (command.currentSlot == command.correctSlot)
            {
                command.GetComponent<Image>().color = ColorUtility.TryParseHtmlString("#006A62", out var cor) ? cor : Color.white;
                acertos++;
            }
            else command.GetComponent<Image>().color = Color.red;
        }
        Debug.Log("Acertos Puzzle: " + acertos);

        if (acertos == commands.Length)
        {
            Debug.Log("Terminou Fase");
            manager.TerminouNivel();
        }

        acertos = 0;
    }
}
