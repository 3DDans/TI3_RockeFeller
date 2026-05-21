using TMPro;
using UnityEngine;

public class MicroscopeController : MonoBehaviour
{
    [Header("Movement")]
    public float maxMoveSpeed = 2f;
    public float minMoveSpeed = 0.5f;

    [Header("Limits")]
    public Vector2 limitX;
    public Vector2 limitZ;

    [Header("Light System")]
    public UnityEngine.UI.Image darkOverlay;
    public UnityEngine.UI.Image lightOverlay;
    private float lux;

    [Header("Initial Values")]
    public float initialLightValue = 50f;
    public float initialFocusValue = 50f;

    [Range(0, 100)]
    public float lightValue = 1f;

    public float lightChangeSpeed = 30f;

    [Header("Zoom")]
    public MicroscopeZoom zoomController;

    [Header("Focus System")]

    [Range(0, 100)]
    public float focusValue = 50f;

    [Range(0, 100)]
    public float idealFocusValue = 50f;

    public float focusChangeSpeed = 40f;

    public float maxDriftStrength = 2f;

    public float driftSpeed = 1.5f;

    public float deadZone = 0.5f;



    [Header("UI")]
    public UnityEngine.UI.Slider focusSlider;
    public TextMeshProUGUI luxTxt;

    private Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;

        lightValue = initialLightValue;
        focusValue = initialFocusValue;
        focusSlider.value = focusValue;


        UpdateLightVisual();
    }

    void Update()
    {
        MoveMicroscope();
        HandleLight();
        HandleFocus();
        UpdateUI();
    }

    void MoveMicroscope()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float zoomPercent =
            zoomController.GetZoomPercent();

        float currentSpeed =
            Mathf.Lerp(
                maxMoveSpeed,
                minMoveSpeed,
                zoomPercent
            );

        Vector3 move = new Vector3(
            -mouseX * currentSpeed,
            0,
            -mouseY * currentSpeed
        );
        ApplyFocusDrift(ref move);
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
    void HandleLight()
    {
        float lightInput = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
            lightInput = 1f;

        if (Input.GetKey(KeyCode.DownArrow))
            lightInput = -1f;

        float lightMultiplier =
            Mathf.Lerp(
                0.3f,
                1.5f,
                Mathf.Abs(lightValue - 50f) / 50f
            );

        lightValue +=
            lightInput *
            lightChangeSpeed *
            lightMultiplier *
            Time.deltaTime;

        lux =
    Mathf.Lerp(
        0f,
        1000f,
        lightValue / 100f
    );

        UpdateLightVisual();
    }
    void UpdateLightVisual()
    {
        Color darkColor = darkOverlay.color;
        Color lightColor = lightOverlay.color;

        // ESCURO
        if (lightValue < 50f)
        {
            float alpha =
                Mathf.InverseLerp(50f, 0f, lightValue);

            darkColor.a = alpha;
            lightColor.a = 0f;
        }

        // CLARO
        else if (lightValue > 50f)
        {
            float alpha =
                Mathf.InverseLerp(50f, 100f, lightValue);

            darkColor.a = 0f;
            lightColor.a = alpha;
        }

        // IDEAL
        else
        {
            darkColor.a = 0f;
            lightColor.a = 0f;
        }

        darkOverlay.color = darkColor;
        lightOverlay.color = lightColor;
    }

    void HandleFocus()
    {
        float focusInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            focusInput = -1f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            focusInput = 1f;
        }

        float distanceToIdeal =
            Mathf.Abs(
                focusValue - idealFocusValue
            );

        // QUANTO MAIS PERTO DO IDEAL
        // MAIS SENSÍVEL
        float focusMultiplier =
            Mathf.Lerp(
                2.5f, // muito rápido perto
                0.3f, // lento longe
                distanceToIdeal / 50f
            );

        focusValue +=
            focusInput *
            focusChangeSpeed *
            focusMultiplier *
            Time.deltaTime;

        focusValue =
            Mathf.Clamp(
                focusValue,
                0f,
                100f
            );
    }

    void UpdateUI()
    {
        focusSlider.value = focusValue;
        luxTxt.text =
    "LIGHT " +
    Mathf.RoundToInt(lux) +
    " lux";
    }


    void ApplyFocusDrift(ref Vector3 move)
    {
        float difference =
            focusValue - idealFocusValue;

        float intensity =
        Mathf.InverseLerp(
            deadZone,
            50f,
            Mathf.Abs(difference)
        );

        if (intensity <= 0.01f)
            return;

        float drift =
            (
            Mathf.PerlinNoise(
                Time.time * driftSpeed,
                0f
            ) * 2f
) -1f;

        drift *= maxDriftStrength * intensity;

        // ABAIXO DO IDEAL
        if (focusValue < idealFocusValue)
        {
            move.x += drift;
        }

        // ACIMA DO IDEAL
        else
        {
            move.z += drift;
        }
    }

}