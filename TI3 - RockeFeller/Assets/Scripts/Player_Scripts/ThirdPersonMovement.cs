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

    public AudioSource audioSource;
    public AudioClip[] clips;
    private bool isWalkingSoundPlaying = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main.transform;
    }

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
            PlayJumpSound();
        }
        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = inputDir * walkSpeed + velocity;

        characterController.Move(finalMove * Time.deltaTime);

        if (horizontal != 0 || vertical != 0)
        {
            if (!isWalkingSoundPlaying)
            {
                PlayWalkingSound();
            }
        }
        else
        {
            if (isWalkingSoundPlaying)
            {
                audioSource.Stop();
                isWalkingSoundPlaying = false;
            }
        }
    }

    void Sounds(int clip, bool loop) 
    {
        if (audioSource.clip != clips[clip] || !audioSource.isPlaying)
        {
            audioSource.clip = clips[clip];
            audioSource.loop = loop;
            audioSource.Play();

            isWalkingSoundPlaying = loop;
        }
    }

    void PlayWalkingSound()
    {
        Sounds(0, true);
        isWalkingSoundPlaying = true;
    }

    void PlayJumpSound()
    {
        bool wasWalking = isWalkingSoundPlaying;

        if (wasWalking)
        {
            audioSource.Stop();
        }

        Sounds(1, false);
        audioSource.Play();

        if (wasWalking)
        {
            Invoke(nameof(ResumeWalkingSound), clips[1].length);
        }
    }

    void ResumeWalkingSound()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if ((horizontal != 0 || vertical != 0) && !audioSource.isPlaying)
        {
            PlayWalkingSound();
        }
    }
}
