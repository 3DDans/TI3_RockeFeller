using UnityEngine;

public class FootstepsController : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = target.transform.position;

        move.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, move, 4.5f);
    }
}
