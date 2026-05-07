using System;
using UnityEngine;
using TMPro;
using System.Collections;

public class TabletController : MonoBehaviour

{
    [Tooltip("0 = Menu l\n1 = Tela vermelha\n2 = Tela verde\n3 = Tela amarela")]
   

    public GameObject[] Screens;
   public int actualScreen = 0;
   public TextMeshProUGUI hours;
   public GameObject tabletUI;
    private bool isTabletOpen = false;

    void Start()
    {
        // Cursor.lockState = isTabletOpen ? CursorLockMode.None : CursorLockMode.Locked;
        // Cursor.visible = isTabletOpen;
        if (Screens.Length > 0)
        Screens[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Keyboard.current.escapeKey.wasPressedThisFrame)
        // {
        //     ToggleTablet();
        // }

        hours.text = DateTime.Now.ToString("hh:mm tt");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleTablet();
        }
    }
   
    public void ToggleTablet()
    {
        isTabletOpen = !isTabletOpen;
        tabletUI.SetActive(isTabletOpen);

        if (!isTabletOpen)
        {
            Menu();
        }
    }
    
    public void Menu()
    {
        Screens[actualScreen].SetActive(false);
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

        yield return new WaitForSeconds(0.7f);

        actualScreen = index;

        Screens[index].SetActive(true);
        Screens[0].SetActive(false);

        isRunningAnim = false;
    }


}

