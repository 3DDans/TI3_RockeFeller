using JetBrains.Annotations;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class DoorTeleporter : MonoBehaviour
{
    public Transform spawnPos;
    public AutomaticDoorsController doorsController;

    [Header("Area")]
    public TaskArea destinationArea;

    private void Start()
    {
        doorsController = gameObject.transform.parent.GetComponent<AutomaticDoorsController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Teleport(other));
        }
    }

    IEnumerator Teleport(Collider other)
    {
        Debug.Log("Entrou no Trigger de Teleporte!");
        TransitionController.instance.FadeOut();

        ThirdPersonMovement movement = other.GetComponent<ThirdPersonMovement>();
        CharacterController controller = other.GetComponent<CharacterController>();
        GameObject camObj = GameObject.Find("PlayerCamera");
        CinemachineOrbitalFollow orbFol = camObj.GetComponent<CinemachineOrbitalFollow>();
        CinemachineInputAxisController camInput = camObj.GetComponent<CinemachineInputAxisController>();

        movement.canMove = false;
        controller.enabled = false;
        camInput.enabled = false;

        yield return new WaitForSeconds(1f);

        other.transform.position = spawnPos.position; //Teleporta o player e coloca ele na posicao e rotacao do ponto de spawn.
        other.transform.rotation = spawnPos.rotation;

        if (doorsController != null)
            doorsController.FecharPorta();

        PetManager.Instance.TeleportPet();

        orbFol.HorizontalAxis.Value = other.transform.eulerAngles.y; //Reseta a posicao da camera
        orbFol.VerticalAxis.Value = 0;

        yield return new WaitForSeconds(0.2f);

        TransitionController.instance.FadeIn();
        AreaManager.Instance.SetArea(destinationArea);
        AreaNameUI.Instance.ShowArea(AreaManager.Instance.GetAreaName(destinationArea));

        yield return new WaitForSeconds(0.5f);

        
        if (GameProgressManager.Instance.CheckAllPhasesCompletion() && !GameProgressManager.Instance.cutscenePostPuzzlePlayed)
        {
            GameProgressManager.Instance.cutscenePostPuzzlePlayed = true;

            bool finished = false;
            GameProgressManager.Instance.cutscenePostPuzzle.stopped += OnStopped;
            GameProgressManager.Instance.cutscenePostPuzzle.Play();

            yield return new WaitUntil(() => finished);

            GameProgressManager.Instance.cutscenePostPuzzle.stopped -= OnStopped;

            yield return new WaitUntil(() => finished);

            camInput.enabled = true;
            movement.canMove = true;
            controller.enabled = true;

            void OnStopped(PlayableDirector d)
            {
                finished = true;
            }
        }
        else
        {
            camInput.enabled = true;
            movement.canMove = true;
            controller.enabled = true;
        }
    }
}
