using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    float currentX = 0f;
    float currentY = 0f;

    public float smoothSpeed = 5f; // controla suavidade

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float targetX = 0f;
        float targetY = 0f;

        if (Keyboard.current.aKey.isPressed) targetX = -1;
        if (Keyboard.current.dKey.isPressed) targetX = 1;
        if (Keyboard.current.wKey.isPressed) targetY = 1;
        if (Keyboard.current.sKey.isPressed) targetY = -1;

        // suavização
        currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * smoothSpeed);
        currentY = Mathf.Lerp(currentY, targetY, Time.deltaTime * smoothSpeed);

        animator.SetFloat("moveX", currentX);
        animator.SetFloat("moveY", currentY);
    }
}