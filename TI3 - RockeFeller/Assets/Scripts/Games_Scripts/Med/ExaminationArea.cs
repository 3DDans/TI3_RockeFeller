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
    public Renderer meshRenderer;
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;

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

        meshRenderer.material.color = normalColor;
    }

    void Update()
    {
        HandleHover();
        HandleScan();
    }

    void HandleHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isHovering = true;

                bool hasTool =
                    MedicalToolManager.Instance.HasTool();

                meshRenderer.material.color =
                    hasTool ? highlightColor : Color.red;

                return;
            }
        }

        isHovering = false;
        meshRenderer.material.color = normalColor;
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

        // COMEÇA O SCAN
        if (Input.GetMouseButtonDown(0) && !isScanning)
        {
            StartScan();
        }

        // SEGURANDO
        if (Input.GetMouseButton(0) && isScanning)
        {
            currentTime += Time.deltaTime;
            scanSlider.value = currentTime / scanTime;

            if (currentTime >= scanTime)
            {
                FinishScan();
            }
        }

        // SOLTOU ANTES
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