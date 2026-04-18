using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 5f;
    public Transform cameraTransform;

    [Header("Mouse")]
    public float mouseSensitivity = 150f;
    float yRotation = 0f;

    [Header("Pulo")]
    public float jumpHeight = 2.5f;
    public float gravity = -12f;

    public bool canMove = true;

    public CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    public AudioSource[] audioSource;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!canMove) return;

        Move();
        RotateWithMouse();
    }

    void Move()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * v + right * h;

        // 👉 ANIMAÇÃO DE ANDAR
        bool isWalking = h != 0 || v != 0;
        animator.SetBool("isWalking", isWalking);

        // PULO
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
            audioSource[1].Play();
        }

        audioSource[1].Stop();

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = move * moveSpeed + velocity;

        if (h != 0 || v != 0)
        {
            audioSource[0].Play();
        }
        else 
        {
            audioSource[0].Stop();
        }

        controller.Move(finalMove * Time.deltaTime);
    }

    void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}