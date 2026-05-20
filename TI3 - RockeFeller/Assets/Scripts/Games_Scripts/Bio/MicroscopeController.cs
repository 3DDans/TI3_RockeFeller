using UnityEngine;

public class MicroscopeController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Limits")]
    public Vector2 limitX;
    public Vector2 limitZ;

    private Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        MoveMicroscope();
    }

    void MoveMicroscope()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 move = new Vector3(
            -mouseX * moveSpeed,
            0,
            -mouseY * moveSpeed
        );

        transform.position += move * Time.deltaTime;

        transform.position = new Vector3(
            Mathf.Clamp(
                transform.position.x,
                initialPos.x + limitX.x,
                initialPos.x + limitX.y
            ),

            transform.position.y,

            Mathf.Clamp(
                transform.position.z,
                initialPos.z + limitZ.x,
                initialPos.z + limitZ.y
            )
        );
    }
}