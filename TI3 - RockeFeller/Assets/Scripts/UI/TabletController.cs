using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TabletController : MonoBehaviour
{
    public GameObject[] Screens;
    public int actualScreen = 0;
    public TextMeshProUGUI hours;
    public GameObject tabletUI;

    [HideInInspector]
    public bool isInMainMenu = true;

    private bool isTabletOpen;
    public bool canPause = false;

    [Header("Task UI")]
    public TabletTaskListUI taskListUI;

    void Start()
    {
        isTabletOpen = false;
        tabletUI.SetActive(false);

        if (Screens.Length > 0)
        {
            Screens[0].SetActive(true);
        }
    }

    void Update()
    {
        hours.text = DateTime.Now.ToString("hh:mm tt");

        if (Input.GetKeyDown(KeyCode.Tab) &&
            !GameProgressManager.IsInMinigame &&
            canPause)
        {
            ToggleTablet();
        }
    }

    public void ToggleTablet()
    {
        isTabletOpen = !isTabletOpen;

        tabletUI.SetActive(isTabletOpen);

        if (isTabletOpen)
        {
            taskListUI.RefreshTasks();
        }

        if (isTabletOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            if (isInMainMenu)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        Time.timeScale = isTabletOpen ? 0f : 1f;
    }

    public void Menu()
    {
        Screens[actualScreen].SetActive(false);
        actualScreen = 0;
        Screens[actualScreen].SetActive(true);
    }

    public void OpenScreen(int index)
    {
        if (index < 0 || index >= Screens.Length)
            return;

        if (actualScreen == index)
            return;

        Screens[actualScreen].SetActive(false);
        actualScreen = index;
        Screens[actualScreen].SetActive(true);

        Debug.Log("Screen " + index + " opened.");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Punkofeller");
    }
}