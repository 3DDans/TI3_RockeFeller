using UnityEngine;

public class OpeningMenu : MonoBehaviour //Menu temporário
{
    public GameObject Canvas;
    public GameObject Menu;
    private PauseMenu PauseMenu;
    private void Awake()
    {
        PauseMenu=this.GetComponent<PauseMenu>();
    }

    public void Start()
    {
        CursorManager.Instance.ShowCursor();
    }

    public void Play()
    {
        CursorManager.Instance.HideCursor();
        Canvas.SetActive(true);
        Menu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnMenu()
    {
        Canvas.SetActive(false);
        Menu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Pause()
    {
        PauseMenu.Pause();
    }
    public void Quit()
    {     
        Application.Quit(); 
    }
}
