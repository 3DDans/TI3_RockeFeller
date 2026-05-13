using UnityEngine;

public class PartJiggle : MonoBehaviour
{
    public float amplitude = 0.05f;
    public float speed = 3f;

    private Vector3 startPos;
    private bool isFloating = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (!isFloating) return;

        float y = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = startPos + new Vector3(0, y, 0);
    }

    public void StartFloat()
    {
        startPos = transform.position;
        isFloating = true;
    }

    public void StopFloat()
    {
        isFloating = false;
        transform.position = startPos;
    }
}       