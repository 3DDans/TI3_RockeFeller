using System.Text;
using TMPro;
using UnityEngine;

public class MissionGuideUI : MonoBehaviour
{
    public TextMeshProUGUI taskText;

    private void Update()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        StringBuilder sb = new StringBuilder();

        TaskArea currentArea = AreaManager.Instance.CurrentArea;

        //sb.AppendLine("=== MAIN QUESTS ===");
        //sb.AppendLine();

        BuildSection(sb, TaskType.Principal, currentArea);

        //sb.AppendLine();
        //sb.AppendLine("=== SIDE QUESTS ===");
        //sb.AppendLine();

        //BuildSection(sb, TaskType.Secundaria, currentArea);

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
