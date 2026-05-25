using UnityEngine;

public class SlotPulse : MonoBehaviour
{
    public Vector3 direction = Vector3.right;

    public float amplitude = 0.03f;
    public float speed = 3f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float value =
            Mathf.Sin(Time.time * speed) * amplitude;

        transform.localPosition =
            startPos + direction * value;
    }
}