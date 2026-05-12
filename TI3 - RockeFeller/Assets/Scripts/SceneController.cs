using Unity.VisualScripting;
using UnityEngine;
using Unity.Cinemachine;

public class SceneController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject mainMenu;
    public GameObject hud;

    public CinemachineCamera introCamera;
    public CinemachineCamera playerCamera;

    public MonoBehaviour playerMovement;

    public float introTime = 3f;

    void Start()
    {
        // Menu ativo
        mainMenu.SetActive(true);

        // HUD desligada
        hud.SetActive(false);

        // Travar player
        playerMovement.enabled = false;

        // Prioridades
        introCamera.Priority = 20;
        playerCamera.Priority = 10;
    }

    public void PlayGame()
    {
        // Esconde menu
        mainMenu.SetActive(false);

        // Troca câmera
        introCamera.Priority = 0;
        playerCamera.Priority = 20;

        // Espera blend terminar
        Invoke(nameof(StartGameplay), introTime);
    }

    void StartGameplay()
    {
        // Libera player
        playerMovement.enabled = true;

        // Liga HUD
        hud.SetActive(true);
    }
}
