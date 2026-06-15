using UnityEngine;

public class MenuCameraBob2 : MonoBehaviour
{
    public float verticalAmplitude = 0.1f;
    public float horizontalAmplitude = 0.05f;

    public float rotationAmplitude = 1f;

    public float speed = 0.3f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
    }

    private void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * horizontalAmplitude;

        float y = Mathf.Cos(Time.time * speed * 0.8f) * verticalAmplitude;

        transform.localPosition =
            startPosition + new Vector3(x, y, 0);

        float zRot =
            Mathf.Sin(Time.time * speed * 0.5f) * rotationAmplitude;

        transform.localRotation =
            startRotation * Quaternion.Euler(0, 0, zRot);
    }
}