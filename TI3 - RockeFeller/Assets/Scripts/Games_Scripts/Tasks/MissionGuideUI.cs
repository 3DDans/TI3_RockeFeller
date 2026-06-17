using System.Text;
using TMPro;
using UnityEngine;

public class MissionGuideUI : MonoBehaviour
{
    public GameObject missionsPanel;
    public TextMeshProUGUI taskText;

    private void Start()
    {
        missionsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bool opening = !missionsPanel.activeSelf;

            missionsPanel.SetActive(opening);

            if (opening)
            {
                RefreshUI();
            }
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
            bool correctType = task.type == type;
            bool available = task.status == TaskStatus.Available;
            bool correctArea = task.area == currentArea ||
                               task.area == TaskArea.Global;

            if (correctType && available && correctArea)
            {
                sb.AppendLine("• " + task.taskName);
            }
        }
    }
}