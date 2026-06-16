using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    public static bool HasInstance => Instance != null;
    [Header("Todas as Tasks do Jogo")]
    public List<Task> allTasks = new List<Task>();

    private List<string> completedEvents = new List<string>();

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
        CheckTaskUnlocks();
        
    }

    public void RegisterEvent(string eventID)
    {
        if (completedEvents.Contains(eventID))
            return;

        completedEvents.Add(eventID);

        Debug.Log("Evento registrado: " + eventID);

        CheckTaskCompletions(eventID);
        CheckTaskUnlocks();
    }

    private void CheckTaskCompletions(string eventID)
    {
        foreach (Task task in allTasks)
        {
            if (task.status != TaskStatus.Available)
                continue;

            if (task.completeEventID == eventID)
            {
                task.status = TaskStatus.Completed;

                Debug.Log("Task concluída: " + task.taskName);
            }
        }
    }

    private void CheckTaskUnlocks()
    {
        foreach (Task task in allTasks)
        {
            if (task.status != TaskStatus.Locked)
                continue;

            bool canUnlock = true;

            foreach (string requiredEvent in task.unlockEventIDs)
            {
                if (!completedEvents.Contains(requiredEvent))
                {
                    canUnlock = false;
                    break;
                }
            }

            if (canUnlock)
            {
                task.status = TaskStatus.Available;

                Debug.Log("Task desbloqueada: " + task.taskName);
            }
        }
    }

    public Task GetTaskByID(string taskID)
    {
        foreach (Task task in allTasks)
        {
            if (task.taskID == taskID)
                return task;
        }

        return null;
    }
}