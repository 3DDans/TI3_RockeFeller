using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIButtonFX : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Hover")]
    public float hoverScale = 1.08f;
    public float hoverMoveY = 2f;

    [Header("Click")]
    public float clickScale = 0.92f;
    public float bounceScale = 1.12f;
    public float bounceDuration = 0.08f;

    [Header("Animation")]
    public float speed = 10f;

    [Header("Glow")]
    public Image glowImage;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private Vector3 originalScale;
    private Vector3 originalPosition;

    private Vector3 targetScale;
    private Vector3 targetPosition;

    private bool isHovering;
    private bool hovering;
    private bool isBouncing;

    private Coroutine bounceRoutine;
    private Coroutine pulseRoutine;

    private Color originalGlowColor;

    void Awake()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;

        targetScale = originalScale;
        targetPosition = originalPosition;

        if (glowImage != null)
        {
            originalGlowColor = glowImage.color;
        }
    }

    void Update()
    {
        if (!isBouncing)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                targetScale,
                Time.unscaledDeltaTime * speed
            );
        }

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            Time.unscaledDeltaTime * speed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHovering) return;

        isHovering = true;
        hovering = true;

        targetScale = originalScale * hoverScale;

        targetPosition = originalPosition + new Vector3(0, hoverMoveY, 0);

        if (glowImage != null)
        {
            if (pulseRoutine != null)
                StopCoroutine(pulseRoutine);

            pulseRoutine = StartCoroutine(PulseGlow());
        }

        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        hovering = false;

        targetScale = originalScale;
        targetPosition = originalPosition;

        if (glowImage != null)
        {
            if (pulseRoutine != null)
                StopCoroutine(pulseRoutine);

            glowImage.color = originalGlowColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (bounceRoutine != null)
        {
            StopCoroutine(bounceRoutine);
        }

        transform.localScale = originalScale * clickScale;

        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (bounceRoutine != null)
        {
            StopCoroutine(bounceRoutine);
        }

        bounceRoutine = StartCoroutine(BounceBack());
    }

    IEnumerator BounceBack()
    {
        isBouncing = true;

        float timer = 0;

        Vector3 startScale = transform.localScale;
        Vector3 overshootScale = originalScale * bounceScale;

        while (timer < bounceDuration)
        {
            timer += Time.unscaledDeltaTime;

            transform.localScale = Vector3.Lerp(
                startScale,
                overshootScale,
                timer / bounceDuration
            );

            yield return null;
        }

        timer = 0;

        Vector3 target =
            hovering ?
            originalScale * hoverScale :
            originalScale;

        while (timer < bounceDuration)
        {
            timer += Time.unscaledDeltaTime;

            transform.localScale = Vector3.Lerp(
                overshootScale,
                target,
                timer / bounceDuration
            );

            yield return null;
        }

        transform.localScale = target;

        isBouncing = false;
    }

    IEnumerator PulseGlow()
    {
        while (true)
        {
            float timer = 0f;

            // Aumenta brilho
            while (timer < 0.5f)
            {
                timer += Time.unscaledDeltaTime;

                float alpha = Mathf.Lerp(
                    originalGlowColor.a,
                    1f,
                    timer / 0.5f
                );

                Color c = originalGlowColor;
                c.a = alpha;

                glowImage.color = c;

                yield return null;
            }

            timer = 0f;

            // Diminui brilho
            while (timer < 0.5f)
            {
                timer += Time.unscaledDeltaTime;

                float alpha = Mathf.Lerp(
                    1f,
                    originalGlowColor.a,
                    timer / 0.5f
                );

                Color c = originalGlowColor;
                c.a = alpha;

                glowImage.color = c;

                yield return null;
            }
        }
    }
}