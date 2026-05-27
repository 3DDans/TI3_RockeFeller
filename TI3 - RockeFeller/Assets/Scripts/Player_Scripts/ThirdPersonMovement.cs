using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    CharacterController characterController;
    Transform mainCamera;
    Vector3 velocity;

    public bool canMove = true;

    [Header("Movement")]
    public float rotationSpeed;
    public float walkSpeed;
    public float jumpHeight;
    public float gravity;

    [Header("Footstep")]
    public float footstepInterval = 0.45f;

    [Header("VFX")]
    public Transform footstepPoint;

    private float footstepTimer;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        if (canMove)
        {
            Movement();
        }
    }

    void Movement()
    {
        // Leitura de Input e Direcao Relativa a Camera
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 camForward = mainCamera.forward;
        Vector3 camRight = mainCamera.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 inputDir = (camForward * vertical + camRight * horizontal);

        // Rotacao do personagem
        if (inputDir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDir);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Som de passos
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0)
            {
                SoundFXManager.Instance.PlaySFX("footstep");

                VFXManager.Instance.PlayVFX(
        "FootstepDust",
        footstepPoint.position
    );

                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0;
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            SoundFXManager.Instance.PlaySFX("jump");
        }

        // Gravidade
        velocity.y += gravity * Time.deltaTime;

        // Movimento final
        Vector3 finalMove = inputDir * walkSpeed + velocity;

        characterController.Move(finalMove * Time.deltaTime);
    }
}