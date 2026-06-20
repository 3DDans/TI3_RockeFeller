using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Vector3 lastPosition;
    Vector3 currentSpeed;
    Animator animator;

    CharacterController controller;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        lastPosition = transform.position;
    }

    void Update()
    {
        // Velocidade horizontal
        currentSpeed = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        currentSpeed.y = 0;

        animator.SetFloat("Speed", currentSpeed.magnitude);

        // Checar chão
        animator.SetBool("isGrounded", controller.isGrounded);
    }

    public void TriggerJump()
    {
        animator.SetBool("isJumping", true);
    }

    public void ResetJump()
    {
        animator.SetBool("isJumping", false);
    }
}