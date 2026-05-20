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

    [Header("Detection")]
    public float detectionRadius = 80f;

    [Header("Scan")]
    public float scanTimeRequired = 2f;

    private float currentScanTime = 0f;
    bool alreadyDetected = false;

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
        if (isTarget)
        {
            CheckDetection();
        }
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

    void CheckDetection()
    {
        Camera cam = Camera.main;

        Vector3 screenPos =
            cam.WorldToScreenPoint(transform.position);

        Vector2 screenCenter = new Vector2(
            Screen.width / 2f,
            Screen.height / 2f
        );

        float distance =
            Vector2.Distance(screenPos, screenCenter);

        bool isInside =
            distance <= detectionRadius;

        if (isInside)
        {
            currentScanTime += Time.deltaTime;

            if (currentScanTime >= scanTimeRequired)
            {
                Detect();
            }
        }
        else
        {
            currentScanTime = 0f;
        }
    }

    void Detect()
    {
        if (alreadyDetected)
            return;

        alreadyDetected = true;

        if (isTarget)
        {
            manager.CorrectChoice();
        }
        else
        {
            manager.WrongChoice();
        }
    }
}