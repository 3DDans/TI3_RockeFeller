using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private Transform ballSpawn;
    [SerializeField] private AudioSource goalSound;
    public Transform vfxPoint, vfxPoint2;
    public GameObject particle;

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
            Particle(vfxPoint);
            Particle(vfxPoint2);

    }

    public void Particle(Transform point)
    {
        GameObject hit = Instantiate(particle, point.transform.position, point.rotation);
        Debug.Log("Instanciou");
        Destroy(hit, 2f);
    }
}