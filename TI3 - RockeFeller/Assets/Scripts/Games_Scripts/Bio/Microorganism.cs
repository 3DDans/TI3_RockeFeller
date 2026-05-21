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
    private Vector3 currentDirection;

    [Header("Animation")]

    public Transform[] bones;
    private Quaternion[] boneInitialRotations;

    [Header("Pulse")]
    public bool usePulse = true;
    public float pulseSpeed = 2f;
    public float pulseAmount = 0.05f;

    [Header("Wobble")]
    public bool useWobble = true;
    public float wobbleSpeed = 3f;
    public float wobbleAmount = 15f;

    [Header("Bone Follow")]
    public float followDelay = 0.15f;

    private Vector3 baseScale;

    void Start()
    {
        manager = FindFirstObjectByType<BiologyGameManager>();

        baseScale = transform.localScale;

        // Randomização
        pulseSpeed += Random.Range(-0.5f, 0.5f);

        wobbleSpeed += Random.Range(-1f, 1f);

        wobbleAmount += Random.Range(-5f, 5f);
        PickNewDirection();
        currentDirection = direction;
        boneInitialRotations = new Quaternion[bones.Length];

        for (int i = 0; i < bones.Length; i++)
        {
            boneInitialRotations[i] = bones[i].localRotation;
        }
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
        if (!BiologyGameManager.gameStarted)
            return;

        Move();

        AnimateVisuals();

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

        currentDirection = Vector3.Lerp(
            currentDirection,
            direction,
            Time.deltaTime * 2f
        ).normalized;

        Vector3 pos = transform.position;

        pos += currentDirection * speed * Time.deltaTime;

        float margin = 0.3f;

        pos.x = Mathf.Clamp(
            pos.x,
            limitX.x + margin,
            limitX.y - margin
        );

        pos.z = Mathf.Clamp(
            pos.z,
            limitZ.x + margin,
            limitZ.y - margin
        );

        // trava altura
        pos.y = transform.position.y;

        transform.position = pos;
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

    void AnimateVisuals()
    {
        AnimatePulse();

        AnimateBones();
    }

    void AnimatePulse()
    {
        if (!usePulse)
            return;

        float pulse =
            1f +
            Mathf.Sin(Time.time * pulseSpeed)
            * pulseAmount;

        transform.localScale =
            baseScale * pulse;
    }

    void AnimateBones()
    {
        if (!useWobble)
            return;

        if (bones == null || bones.Length == 0)
            return;

        for (int i = 0; i < bones.Length; i++)
        {
            float offset = i * followDelay;

            float wave =
                Mathf.Sin(
                    (Time.time - offset)
                    * wobbleSpeed
                );

            float rotation =
                wave * wobbleAmount;

            Quaternion wobbleRotation =
                Quaternion.Euler(
                    rotation*0.5f,
                    -rotation,
                    rotation
                );

            bones[i].localRotation =
                boneInitialRotations[i] * wobbleRotation;
        }
    }


}