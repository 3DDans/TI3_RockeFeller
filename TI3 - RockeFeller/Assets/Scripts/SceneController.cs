using Unity.VisualScripting;
using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SceneController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject mainMenu;
    public GameObject hud;

    public CinemachineCamera introCamera;
    public CinemachineCamera playerCamera;

    public MonoBehaviour playerMovement;
    public CharacterController playerCC;

    public float introTime = 3f;

    [Header("Cutscenes")]
    public bool tocarCutscene = true;
    public PlayableDirector cutscenePlay;

    void Start()
    {
        // Menu ativo
        mainMenu.SetActive(true);

        // HUD desligada
        hud.SetActive(false);

        // Travar player
        playerCC.enabled = false;
        playerMovement.enabled = false;

        // Prioridades
        introCamera.Priority = 20;
        playerCamera.Priority = 10;
       
    }

    public void PlayGame()
    {
        // Esconde menu
        mainMenu.SetActive(false);
        CursorManager.Instance.HideCursor();
        hud.SetActive(true);

        if (tocarCutscene)
        {
            StartCoroutine(PlayCutscene());
            
            TransitionController.instance.FadeIn();
            playerCC.enabled = true;
            playerMovement.enabled = true;
        }
        else
        {
            playerCC.enabled = false;
            playerMovement.enabled = false;

            playerCC.transform.position = new Vector3(0.08f, 0.323f, -25.3f);

            playerCC.enabled = true;
            playerMovement.enabled = true;
        }
        introCamera.Priority = 0;
        playerCamera.Priority = 20;
    }

    IEnumerator PlayCutscene()
    {
        bool finished = false;

        cutscenePlay.stopped += OnStopped;
        cutscenePlay.Play();

        yield return new WaitUntil(() => finished);

        cutscenePlay.stopped -= OnStopped;

        TimelineAsset timeline = (TimelineAsset)cutscenePlay.playableAsset;
        foreach (var track in timeline.GetOutputTracks())
        {
            if(track.name == "PlayerAnimation" || track.name == "PlayerMovement")
            {
                cutscenePlay.SetGenericBinding(track, null);
            }
        }

        cutscenePlay.gameObject.SetActive(false);

        void OnStopped(PlayableDirector d)
        {
            finished = true;
        }
    }

    void StartGameplay()
    {
        // Libera player
        playerMovement.enabled = true;

        // Liga HUD
        hud.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Fechando jogo...");

        Application.Quit();

    }
}
