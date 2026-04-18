using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Vector3 lastPosition;
    Vector3 currentSpeed;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        currentSpeed = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        currentSpeed.y = 0;

        animator.SetFloat("Speed", currentSpeed.magnitude);
    }
}
