using System;

[Serializable]
public class MinigameData
{
    public MinigameID id;

    public bool unlocked;
    public bool completed;

    public MinigameData(MinigameID minigameID, bool startsUnlocked)
    {
        id = minigameID;
        unlocked = startsUnlocked;
        completed = false;
    }
}