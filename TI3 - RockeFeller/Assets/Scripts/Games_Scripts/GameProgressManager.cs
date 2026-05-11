using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;

    [Header("Game State")]
    public GamePhase currentPhase;

    [Header("Minigames")]
    public List<MinigameData> minigames = new();

    [Header("Linear Progression")]
    public int currentLinearStage = 0;

    [Header("Meteor")]
    public bool meteorUnlocked = false;

[Header("End Game")]
public bool finalStageUnlocked = false;
public bool gameFinished = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeMinigames();
    }

    private void InitializeMinigames()
    {
        minigames.Add(new MinigameData(MinigameID.Medicina, false));
        minigames.Add(new MinigameData(MinigameID.Biologia, false));
        minigames.Add(new MinigameData(MinigameID.Engenharia, false));
        minigames.Add(new MinigameData(MinigameID.Programacao, false));

        minigames.Add(new MinigameData(MinigameID.AnaliseMeteoro, false));
        minigames.Add(new MinigameData(MinigameID.Telescopio, false));
        minigames.Add(new MinigameData(MinigameID.Rover, false));
        minigames.Add(new MinigameData(MinigameID.ProgramacaoRover, false));

        currentPhase = GamePhase.FirstPhase;
    }

    public void CompleteMinigame(MinigameID id)
    {
        MinigameData minigame = minigames.Find(m => m.id == id);

        if (minigame == null)
            return;

        minigame.completed = true;

        Debug.Log($"{id} completed!");

        CheckProgression();
    }

    private void CheckProgression()
    {
        CheckFirstPhaseCompletion();
        CheckSecondPhaseProgression();
    }

    private void CheckFirstPhaseCompletion()
    {
        bool allCompleted =
            IsCompleted(MinigameID.Medicina) &&
            IsCompleted(MinigameID.Biologia) &&
            IsCompleted(MinigameID.Engenharia) &&
            IsCompleted(MinigameID.Programacao);

        if (allCompleted && !meteorUnlocked)
        {
            UnlockMeteor();
        }
    }

    private void UnlockMeteor()
    {
        meteorUnlocked = true;

        currentPhase = GamePhase.MeteorUnlocked;

        UnlockMinigame(MinigameID.AnaliseMeteoro);

        Debug.Log("Meteor unlocked!");
    }

    private void CheckSecondPhaseProgression()
    {
        switch (currentLinearStage)
        {
            case 0:
                if (IsCompleted(MinigameID.AnaliseMeteoro))
                {
                    UnlockMinigame(MinigameID.Telescopio);
                    currentLinearStage++;
                }
                break;

            case 1:
                if (IsCompleted(MinigameID.Telescopio))
                {
                    UnlockMinigame(MinigameID.Rover);
                    currentLinearStage++;
                }
                break;

            case 2:
                if (IsCompleted(MinigameID.Rover))
                {
                    UnlockMinigame(MinigameID.ProgramacaoRover);
                    currentLinearStage++;
                }
                break;

            case 3:
                if (IsCompleted(MinigameID.ProgramacaoRover))
                {
                    currentPhase = GamePhase.Finished;

                    Debug.Log("Game Finished!");
                }
                break;
        }
    }
    public bool IsCompleted(MinigameID id)
    {
        MinigameData minigame = minigames.Find(m => m.id == id);

        return minigame != null && minigame.completed;
    }
    public void UnlockMinigame(MinigameID id)
    {
        MinigameData minigame = minigames.Find(m => m.id == id);

        if (minigame != null)
        {
            minigame.unlocked = true;

            Debug.Log($"{id} unlocked!");
        }
    }
    public bool IsUnlocked(MinigameID id)
    {
        MinigameData minigame = minigames.Find(m => m.id == id);

        return minigame != null && minigame.unlocked;
    }
}