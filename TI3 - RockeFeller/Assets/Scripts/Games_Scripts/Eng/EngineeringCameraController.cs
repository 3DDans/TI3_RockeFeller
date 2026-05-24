using Unity.Cinemachine;
using UnityEngine;

public class EngineeringCameraController : MonoBehaviour
{
    public CinemachineCamera cinemachineCamera;

    private CinemachineOrbitalFollow orbitalFollow;

    [Header("Rotation")]
    public float horizontalSpeed = 350f;
    public float verticalSpeed = 1.5f;

    [Header("Vertical Limits")]
    public float minVertical = 0.15f;
    public float maxVertical = 0.75f;

    private float horizontalAxis;
    private float verticalAxis = 0.45f;

    void Start()
    {
        orbitalFollow = cinemachineCamera.GetComponent<CinemachineOrbitalFollow>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
         
                CursorManager.Instance.HideCursor();
           


            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Horizontal
            horizontalAxis += mouseX * horizontalSpeed * Time.deltaTime;

            // Vertical (bem suave)
            verticalAxis -= mouseY * verticalSpeed * Time.deltaTime;

            // Limites pequenos
            verticalAxis = Mathf.Clamp(verticalAxis, minVertical, maxVertical);
        }
        if (Input.GetMouseButtonUp(1))
        {
            CursorManager.Instance.ShowCursor();
        }
        orbitalFollow.HorizontalAxis.Value = horizontalAxis;
        orbitalFollow.VerticalAxis.Value = verticalAxis;
    }
}