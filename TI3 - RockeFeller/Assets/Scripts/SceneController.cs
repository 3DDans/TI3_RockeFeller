using Unity.VisualScripting;
using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject mainMenu;
    public GameObject hud;
    public GameObject pauseText;

    public CinemachineCamera introCamera;
    public CinemachineCamera playerCamera;

    public MonoBehaviour playerMovement;
    public CharacterController playerCC;

    public float introTime = 3f;
    public TabletController tabletController;

    [Header("Cutscenes")]
    public bool tocarCutscene = true;
    public PlayableDirector cutscenePlay;

    void Start()
    {

        MusicManager.Instance.PlayMusic("music1");
        // Menu ativo
        mainMenu.SetActive(true);

        // HUD desligada
        pauseText.SetActive(false);
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
        

        if (tocarCutscene)
        {
            tabletController.canPause = false;
            StartCoroutine(PlayCutscene());
            
            //TransitionController.instance.FadeIn();
        }
        else
        {
            playerCC.enabled = false;
            playerMovement.enabled = false;

            playerCC.transform.position = new Vector3(0.08f, 0.323f, -25.3f);

            playerCC.enabled = true;
            playerMovement.enabled = true;
            tabletController.canPause = true;
            pauseText.SetActive(true);
            hud.SetActive(true);
        }
        introCamera.Priority = 0;
        playerCamera.Priority = 20;
    }

    IEnumerator PlayCutscene()
    {
        playerCC.enabled = false;
        playerMovement.enabled = false;
        bool finished = false;
        pauseText.SetActive(false);
        hud.SetActive(true);

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
        Debug.Log("Cutscene Finalizada");

        TaskManager.Instance.RegisterEvent("DEAN_FIRST_CONVERSATION");
        playerCC.enabled = true;
        playerMovement.enabled = true;
        pauseText.SetActive(true);
        tabletController.canPause = true;

        cutscenePlay.gameObject.SetActive(false);

        void OnStopped(PlayableDirector d)
        {
            finished = true;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(1);
        
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene(0);

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
