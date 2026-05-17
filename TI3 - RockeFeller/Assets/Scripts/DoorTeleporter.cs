using JetBrains.Annotations;
using Unity.Cinemachine;
using UnityEngine;

public class DoorTeleporter : MonoBehaviour
{
    public Transform spawnPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entrou no Trigger de Teleporte!");
            //Adicionar transição com IEnumerator

            
            ThirdPersonMovement movement = other.GetComponent<ThirdPersonMovement>();
            CharacterController controller = other.GetComponent<CharacterController>();
            GameObject camObj = GameObject.Find("PlayerCamera");
            CinemachineOrbitalFollow orbFol = camObj.GetComponent<CinemachineOrbitalFollow>();
            CinemachineInputAxisController camInput = camObj.GetComponent<CinemachineInputAxisController>();

            movement.canMove = false;
            controller.enabled = false;
            camInput.enabled = false;

            other.transform.position = spawnPos.position; //Teleporta o player e coloca ele na posicao e rotacao do ponto de spawn.
            other.transform.rotation = spawnPos.rotation;

            orbFol.HorizontalAxis.Value = other.transform.eulerAngles.y; //Reseta a posicao da camera
            orbFol.VerticalAxis.Value = 0;

            camInput.enabled = true;
            movement.canMove = true;
            controller.enabled = true;
        }
    }
}
