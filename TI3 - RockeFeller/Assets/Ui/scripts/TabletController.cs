using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class TabletController : MonoBehaviour

{
    public GameObject[] Screens;
   public int actualScreen = 0;
   public TextMeshProUGUI hours;
   public GameObject tabletUI;
    private bool isTabletOpen;

    void Start()
    {
        isTabletOpen = false;
        tabletUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (Screens.Length > 0)
        Screens[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        hours.text = DateTime.Now.ToString("hh:mm tt");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed. Toggling tablet.");
            ToggleTablet();
        }
    }
   
    public void ToggleTablet()
    {
        Debug.Log("Toggling tablet. Current state: " + (isTabletOpen ? "Open" : "Closed"));
        isTabletOpen = !isTabletOpen;
        Debug.Log("New tablet state: " + (isTabletOpen ? "Open" : "Closed"));
        tabletUI.SetActive(isTabletOpen);
        Debug.Log("Tablet UI active: " + tabletUI.activeSelf);
        Cursor.visible = isTabletOpen;
        Cursor.lockState = isTabletOpen
        ? CursorLockMode.None
        : CursorLockMode.Locked;

        Time.timeScale = isTabletOpen ? 0f : 1f;
    }
    
    public void Menu()
    {
        Screens[actualScreen].SetActive(false);
        actualScreen = 0;
        Screens[0].SetActive(true);

    }
    bool isRunningAnim = false;

    public void OpenScreen(int index)
    {
        if (isRunningAnim) return;
        StartCoroutine(OpenScreenRoutine(index));
    }

    IEnumerator OpenScreenRoutine(int index)
    {
        isRunningAnim = true;
        yield return new WaitForSecondsRealtime(0.7f);
        Screens[actualScreen].SetActive(false);
        actualScreen = index;
        Screens[actualScreen].SetActive(true);
        isRunningAnim = false;
        Debug.Log("Screen " + index + " opened.");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Punkofeller");
    }

}

