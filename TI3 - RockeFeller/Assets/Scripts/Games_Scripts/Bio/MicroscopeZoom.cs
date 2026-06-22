using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class MicroscopeZoom : MonoBehaviour
{
    public CinemachineCamera virtualCamera;

    [Header("Zoom")]
    public float zoomSpeed = 10f;

    public float minFOV = 15f;
    public float maxFOV = 50f;

    [Header("HUD")]
    public TextMeshProUGUI zoomText;

    [Header("Fake Zoom Values")]
    public int minZoomValue = 100;
    public int maxZoomValue = 500;

    [Header("Initial State")]
    public float initialFOV = 35f;

    private bool zoomSoundPlaying;
    private float zoomTimer;

    [SerializeField] private float zoomSoundDuration = 14.028f;

    private float targetFOV;
    void Start()
    {
        targetFOV = initialFOV;

        virtualCamera.Lens.FieldOfView =
            initialFOV;

        UpdateZoomHUD();
    }

    void Update()
    {
        HandleZoom();
        SmoothZoom();
        UpdateZoomHUD();
        HandleZoomSound();

        if (zoomSoundPlaying)
        {
            zoomTimer -= Time.deltaTime;

            if (zoomTimer <= 0f)
            {
                zoomSoundPlaying = false;
            }
        }
    }

    void HandleZoom()
    {
        float scroll =
            Input.GetAxis("Mouse ScrollWheel");

        if (scroll == 0)
            return;

        targetFOV -= scroll * zoomSpeed;

        targetFOV = Mathf.Clamp(
            targetFOV,
            minFOV,
            maxFOV
        );
    }

    void SmoothZoom()
    {
        float currentFOV =
            virtualCamera.Lens.FieldOfView;

        currentFOV = Mathf.Lerp(
            currentFOV,
            targetFOV,
            Time.deltaTime * 8f
        );

        virtualCamera.Lens.FieldOfView =
            currentFOV;
    }

    void UpdateZoomHUD()
    {
        float zoomPercent =
            Mathf.InverseLerp(
                maxFOV,
                minFOV,
                virtualCamera.Lens.FieldOfView
            );

        int fakeZoom =
            Mathf.RoundToInt(
                Mathf.Lerp(
                    minZoomValue,
                    maxZoomValue,
                    zoomPercent
                )
            );

        zoomText.text =
            "ZOOM " + fakeZoom + "X";
    }

    public float GetZoomPercent()
    {
        return Mathf.InverseLerp(
            maxFOV,
            minFOV,
            virtualCamera.Lens.FieldOfView
        );
    }

    void HandleZoomSound()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        bool isZooming = Mathf.Abs(scroll) > 0.0001f;

        if (isZooming)
        {
            if (!zoomSoundPlaying)
            {
                SoundFXManager.Instance.PlaySFX("Micro"); // ou ZoomSFX

                zoomSoundPlaying = true;
                zoomTimer = zoomSoundDuration;
            }
        }
    }
}