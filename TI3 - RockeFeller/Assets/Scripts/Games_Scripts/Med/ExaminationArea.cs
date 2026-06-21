using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExaminationArea : MonoBehaviour
{
    public enum AreaType
    {
        Head,
        Throat,
        Body
    }

    public AreaType areaType;

    [Header("UI")]
    public GameObject scanPanel;
    public Slider scanSlider;
    public TextMeshProUGUI statusText;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    [Header("Highlight")]
    public GameObject highlightObject;
    public float blinkSpeed = 8f;

    [Header("Config")]
    public float minScanTime = 3f;
    public float maxScanTime = 7f;

    private float scanTime;
    private float currentTime;
    private bool isHovering = false;
    private bool isScanning = false;

    private MedicalGameManager manager;

    void Start()
    {
        manager = FindFirstObjectByType<MedicalGameManager>();

        scanPanel.SetActive(false);
        resultPanel.SetActive(false);

        if (highlightObject != null)
            highlightObject.SetActive(false);
    }

    void Update()
    {
        if (!MedicalGameManager.IsPlayingMedicalGame)
        {
            if (highlightObject != null)
                highlightObject.SetActive(false);

            return;
        }

        HandleHover();
        HandleScan();
    }

    void HandleHover()
    {
        Ray ray = MedicalToolManager.Instance.medicalCamera
            .ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isHovering = true;

                if (highlightObject != null)
                {
                    bool visible = Mathf.Sin(Time.time * blinkSpeed) > 0;
                    highlightObject.SetActive(visible);
                }

                return;
            }
        }

        isHovering = false;

        if (highlightObject != null)
            highlightObject.SetActive(false);
    }

    string GetExaminationResult()
    {
        MedicalToolType tool =
            MedicalToolManager.Instance.currentTool;

        PatientData patient = manager.currentPatient;

        switch (areaType)
        {
            case AreaType.Head:

                switch (tool)
                {
                    case MedicalToolType.Thermometer:
                        return patient.headThermometer;

                    case MedicalToolType.Stethoscope:
                        return patient.headStethoscope;

                    case MedicalToolType.Flashlight:
                        return patient.headFlashlight;
                }

                break;

            case AreaType.Body:

                switch (tool)
                {
                    case MedicalToolType.Thermometer:
                        return patient.bodyThermometer;

                    case MedicalToolType.Stethoscope:
                        return patient.bodyStethoscope;

                    case MedicalToolType.Flashlight:
                        return patient.bodyFlashlight;
                }

                break;
        }

        return "No data.";
    }

    void HandleScan()
    {
        if (!isHovering) return;

        if (Input.GetMouseButtonDown(0) && !isScanning)
        {
            StartScan();
        }

        if (Input.GetMouseButton(0) && isScanning)
        {
            currentTime += Time.deltaTime;
            scanSlider.value = currentTime / scanTime;

            if (currentTime >= scanTime)
            {
                FinishScan();
            }
        }

        if (Input.GetMouseButtonUp(0) && isScanning)
        {
            CancelScan();
        }
    }

    void StartScan()
    {
        isScanning = true;

        scanTime = Random.Range(minScanTime, maxScanTime);
        currentTime = 0f;

        scanPanel.SetActive(true);
        statusText.text = "Analyzing...";
        scanSlider.value = 0f;
    }

    void FinishScan()
    {
        isScanning = false;
        scanPanel.SetActive(false);

        ShowResult();
    }

    void CancelScan()
    {
        isScanning = false;
        scanPanel.SetActive(false);
    }

    void ShowResult()
    {
        resultPanel.SetActive(true);

        resultText.text = GetExaminationResult();

        StartCoroutine(HideResult());
    }

    IEnumerator HideResult()
    {
        yield return new WaitForSeconds(6f);
        resultPanel.SetActive(false);
    }
}