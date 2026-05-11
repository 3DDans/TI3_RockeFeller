using UnityEngine;

public class Microorganism : MonoBehaviour
{
    public bool isTarget = false;
    public float speed = 1f;
    public float changeDirectionTime = 2f;

    public Vector2 limitX;
    public Vector2 limitZ;

    private Vector3 direction;
    private float timer;
    public Transform petriDish;

    private BiologyGameManager manager;

    void Start()
    {
        manager = FindFirstObjectByType<BiologyGameManager>();
        PickNewDirection();
    }

    void OnMouseDown()
    {
        if (isTarget)
        {
            manager.CorrectChoice();
        }
        else
        {
            manager.WrongChoice();
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        timer += Time.deltaTime;

        if (timer >= changeDirectionTime)
        {
            PickNewDirection();
        }

        transform.position += direction * speed * Time.deltaTime;

        
        Vector3 localPos = petriDish.InverseTransformPoint(transform.position);

        float margin = 0.3f;

        localPos.x = Mathf.Clamp(localPos.x, limitX.x + margin, limitX.y - margin);
        localPos.z = Mathf.Clamp(localPos.z, limitZ.x + margin, limitZ.y - margin);

        transform.position = petriDish.TransformPoint(localPos);
    }

    void PickNewDirection()
    {
        timer = 0f;

        direction = new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f)
        ).normalized;
    }
}