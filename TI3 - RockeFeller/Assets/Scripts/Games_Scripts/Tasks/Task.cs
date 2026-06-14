using System;
using System.Collections.Generic;

[Serializable]
public class Task
{
    public string taskID;

    public string taskName;

    public TaskArea area;

    public TaskType type;

    public TaskStatus status;

    public List<string> unlockEventIDs;

    public string completeEventID;
}

public enum TaskArea
{
    Global,
    Campus,
    Biologia,
    Medicina,
    Engenharia
}

public enum TaskType
{
    Principal,
    Secundaria
}

public enum TaskStatus
{
    Locked,
    Available,
    Completed
}