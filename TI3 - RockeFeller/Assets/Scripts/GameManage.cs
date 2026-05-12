using UnityEngine;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject hud;

    public CinemachineCamera introCamera;
    public CinemachineCamera playerCamera;

    public MonoBehaviour playerMovement;

    public float introTime = 3f;

    void Start()
    {
    
        mainMenu.SetActive(true);

        
        hud.SetActive(false);

        
        playerMovement.enabled = false;

        introCamera.Priority = 30;
        playerCamera.Priority = 10;
    }

    public void PlayGame()
    {
        
        mainMenu.SetActive(false);

        // Troca câmera
        introCamera.Priority = 0;
        playerCamera.Priority = 20;

        // Espera blend terminar
        Invoke(nameof(StartGameplay), introTime);
    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void StartGameplay()
    {
        // Libera player
        playerMovement.enabled = true;

        // Liga HUD
        hud.SetActive(true);
    }
}