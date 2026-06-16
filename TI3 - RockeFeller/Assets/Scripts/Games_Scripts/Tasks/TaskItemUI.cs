using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskItemUI : MonoBehaviour
{
    public Toggle checkbox;
    public TMP_Text taskName;

    public void Setup(Task task)
    {
        switch (task.status)
        {
            case TaskStatus.Completed:

                checkbox.interactable = true;
                checkbox.isOn = true;
                taskName.text = task.taskName;

                break;

            case TaskStatus.Available:

                checkbox.interactable = true;
                checkbox.isOn = false;
                taskName.text = task.taskName;

                break;

            case TaskStatus.Locked:

                checkbox.interactable = false;
                checkbox.isOn = false;
                taskName.text = "???";

                break;
        }
    }
}