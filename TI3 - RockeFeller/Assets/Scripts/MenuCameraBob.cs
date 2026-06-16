using UnityEngine;

public class MenuCameraBob : MonoBehaviour
{
    [Header("Position")]
    public float verticalAmplitude = 0.1f;
    public float horizontalAmplitude = 0.05f;

    [Header("Speed")]
    public float speed = 0.3f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * horizontalAmplitude;

        float y = Mathf.Cos(Time.time * speed * 0.8f) * verticalAmplitude;

        transform.localPosition =
            startPosition + new Vector3(x, y, 0f);
    }
}