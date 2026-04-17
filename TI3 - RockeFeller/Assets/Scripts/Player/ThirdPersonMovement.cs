using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    CharacterController characterController;
    Transform mainCamera;
    Vector3 velocity;

    public bool canMove = true;
    public float rotationSpeed;
    public float walkSpeed;
    public float jumpHeight;
    public float gravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) Movement();
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

        // Rotacao do Personagem
        if (inputDir.magnitude > 0.1f){
            Quaternion targetRotation = Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Movimento Vertical (Pulo e Gravidade)
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = inputDir * walkSpeed + velocity;

        characterController.Move(finalMove * Time.deltaTime);
    }
}
