using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private Transform ballSpawn;
    [SerializeField] private AudioSource goalSound;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;

        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        other.transform.position = ballSpawn.position;

        if (goalSound != null)
            goalSound.Play();
    }
}