using UnityEngine;

public class PetFollower : MonoBehaviour
{
    private Transform player;

    [Header("Follow")]
    public float followSpeed = 4f;

    [Header("Offset")]
    public Vector3 offset = new Vector3(1.2f, 1.0f, -1.2f);

    [Header("Float")]
    public float floatHeight = 0.15f;
    public float floatSpeed = 2f;

    private Vector3 lastPosition;

    void Start()
    {
        player = FindFirstObjectByType<ThirdPersonMovement>().transform;

        lastPosition = transform.position;
    }

    void Update()
    {
        if (player == null)
            return;

        Vector3 targetPosition =
            player.position +
            player.right * offset.x +
            player.forward * offset.z;

        transform.Rotate(
            0,
            50f * Time.deltaTime,
            0,
            Space.Self
             );
        float floating =
            Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        targetPosition.y += offset.y + floating;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        Vector3 direction =
    player.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    Time.deltaTime * 5f
                );
        }

        Vector3 movement =
    transform.position - lastPosition;

        float tilt =
            Mathf.Clamp(
                movement.magnitude * 50f,
                -15f,
                15f
            );

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(
                    tilt,
                    transform.eulerAngles.y,
                    0
                ),
                Time.deltaTime * 4f
            );

        lastPosition = transform.position;
    }
}