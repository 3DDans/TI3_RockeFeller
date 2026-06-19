using System.Text;
using TMPro;
using UnityEngine;

public class MissionGuideUI : MonoBehaviour
{
    public TextMeshProUGUI taskText;
    public GameObject boxMissions;

    private void Start()
    {
        boxMissions.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            boxMissions.SetActive(!boxMissions.activeSelf);
        }

        if (boxMissions.activeSelf)
        {
            RefreshUI();
        }
    }

    void RefreshUI()
    {
        StringBuilder sb = new StringBuilder();

        TaskArea currentArea = AreaManager.Instance.CurrentArea;

        BuildSection(sb, TaskType.Principal, currentArea);

        taskText.text = sb.ToString();
    }

    void BuildSection(StringBuilder sb, TaskType type, TaskArea currentArea)
    {
        TaskManager taskManager = TaskManager.Instance;

        foreach (Task task in taskManager.allTasks)
        {
            bool correctType =
                task.type == type;

            bool available =
                task.status == TaskStatus.Available;

            bool correctArea =
                task.area == currentArea ||
                task.area == TaskArea.Global;

            if (correctType &&
                available &&
                correctArea)
            {
                sb.AppendLine("• " + task.taskName);
            }
        }
    }
}
