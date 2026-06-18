using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    CharacterController characterController;
    Transform mainCamera;
    Vector3 velocity;

    PlayerAnimationController anim;
    Animator animator;

    public bool canMove = true;

    [Header("Movement")]
    public float rotationSpeed;
    public float walkSpeed;
    public float jumpHeight;
    public float gravity;

    [Header("Smoothing")]
    public float smoothTime = 0.1f;
    Vector3 smoothInputDir;
    Vector3 smoothInputVelocity;

    [Header("Footstep")]
    public float footstepInterval = 0.45f;

    [Header("VFX")]
    public Transform footstepPoint;

    private float footstepTimer;

    bool wasGrounded;
    bool jumpPressed;
    bool hasJumped;

    bool lastRaycastGrounded; // 👈 NOVO

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<PlayerAnimationController>();
        animator = GetComponent<Animator>();

        mainCamera = Camera.main.transform;

        animator.SetBool("isGrounded", true);
        animator.SetBool("isJumping", false);

        lastRaycastGrounded = true;
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

        Vector3 targetInputDir = (camForward * vertical + camRight * horizontal);

        smoothInputDir = Vector3.SmoothDamp(
            smoothInputDir,
            targetInputDir,
            ref smoothInputVelocity,
            smoothTime
        );

        Vector3 inputDir = smoothInputDir;

        if (inputDir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDir);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

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

        jumpPressed = Input.GetKey(KeyCode.Space);

        if (jumpPressed && characterController.isGrounded && !hasJumped)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            anim.TriggerJump();
            Debug.Log("JUMP TRIGGERED");

            SoundFXManager.Instance.PlaySFX("jump");

            hasJumped = true;
        }

        if (characterController.isGrounded)
        {
            hasJumped = false;
        }

        if (characterController.isGrounded && !wasGrounded)
        {
            //Debug.Log("LANDED → ResetJump()");
            anim.ResetJump();
        }

        wasGrounded = characterController.isGrounded;

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = inputDir * walkSpeed + velocity;

        characterController.Move(finalMove * Time.deltaTime);

        // ===== RAYCAST =====
        float rayDistance = 1.2f;

        bool grounded = Physics.Raycast(
            transform.position + Vector3.up * 0.2f,
            Vector3.down,
            rayDistance
        );

        //Debug.DrawRay(
            //transform.position + Vector3.up * 0.2f,
            //Vector3.down * rayDistance,
            //grounded ? Color.green : Color.red
        //);

        // 👇 SÓ ATUALIZA SE MUDAR
        if (grounded != lastRaycastGrounded)
        {
            //Debug.Log($"[GROUND CHANGED] Raycast: {grounded}");

            animator.SetBool("isGrounded", grounded);

            //Debug.Log("Animator isGrounded: " + animator.GetBool("isGrounded"));

            lastRaycastGrounded = grounded;
        }
    }
}