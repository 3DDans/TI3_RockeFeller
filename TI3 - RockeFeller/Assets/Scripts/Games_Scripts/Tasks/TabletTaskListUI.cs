using System.Collections.Generic;
using UnityEngine;

public class TabletTaskListUI : MonoBehaviour
{
    public Transform content;
    public GameObject taskItemPrefab;

   

    public void RefreshTasks()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (Task task in TaskManager.Instance.allTasks)
        {
            GameObject obj =
                Instantiate(taskItemPrefab, content);

            TaskItemUI item =
                obj.GetComponent<TaskItemUI>();

            item.Setup(task);
        }
    }
}