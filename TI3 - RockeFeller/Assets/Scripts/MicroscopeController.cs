using UnityEngine;

public class MicroscopeController : MonoBehaviour
{
    public float dragSpeed = 0.1f;
    public Vector2 limitX;
    public Vector2 limitZ;

    private Vector3 lastMousePos;
    private bool dragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
            lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }

        if (dragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePos;

            Vector3 move = new Vector3(-delta.x * dragSpeed, 0, -delta.y * dragSpeed);

            transform.position += move;

            // Limites
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, limitX.x, limitX.y),
                transform.position.y,
                Mathf.Clamp(transform.position.z, limitZ.x, limitZ.y)
            );

            lastMousePos = Input.mousePosition;
        }
    }
}