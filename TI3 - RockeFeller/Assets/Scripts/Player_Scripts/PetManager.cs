using UnityEngine;

public class PetManager : MonoBehaviour
{
    public static PetManager Instance;

    public GameObject petRobot;
    public GameObject buildRobot;



    private Transform player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        player = FindFirstObjectByType<ThirdPersonMovement>().transform;

        if (petRobot != null)
            petRobot.SetActive(false);
    }

    public void SpawnPet(Transform bench)
    {
        petRobot.SetActive(true);
        buildRobot.SetActive(false);

        petRobot.transform.position =
            bench.position;
    }

    public void TeleportPet()
    {
        if (petRobot == null || !petRobot.activeSelf)
            return;

        petRobot.transform.position =
            player.position
            - player.forward * 1.5f
            + Vector3.up * 1f;
    }
}