using UnityEngine;

public class MinigameTabletManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject bottomHintPanel;
    public GameObject tabletPanel;

    [Header("Settings")]
    public KeyCode tabletKey = KeyCode.Tab;

    [Header("State")]
    public bool canOpenTablet = false;

    private bool isTabletOpen = false;

    void Start()
    {
        CloseTabletInstant();
    }

    void Update()
    {
        if (!canOpenTablet)
            return;

        if (Input.GetKeyDown(tabletKey))
        {
            ToggleTablet();
        }
    }

    public void ToggleTablet()
    {
        isTabletOpen = !isTabletOpen;

        tabletPanel.SetActive(isTabletOpen);
        bottomHintPanel.SetActive(!isTabletOpen);

        
    }

    public void OpenTablet()
    {
        isTabletOpen = true;

        tabletPanel.SetActive(true);
        bottomHintPanel.SetActive(false);

       
    }

    public void CloseTablet()
    {
        isTabletOpen = false;

        tabletPanel.SetActive(false);
        bottomHintPanel.SetActive(true);

       
    }

    void CloseTabletInstant()
    {
        isTabletOpen = false;

        if (tabletPanel != null)
            tabletPanel.SetActive(false);

        if (bottomHintPanel != null)
            bottomHintPanel.SetActive(false);
    }

    public void EnableTablet()
    {
        canOpenTablet = true;

        if (!isTabletOpen && bottomHintPanel != null)
        {
            bottomHintPanel.SetActive(true);
        }
    }

    public void DisableTablet()
    {
        canOpenTablet = false;

        if (tabletPanel != null)
            tabletPanel.SetActive(false);

        if (bottomHintPanel != null)
            bottomHintPanel.SetActive(false);

        isTabletOpen = false;
    }

    public bool IsTabletOpen()
    {
        return isTabletOpen;
    }
}