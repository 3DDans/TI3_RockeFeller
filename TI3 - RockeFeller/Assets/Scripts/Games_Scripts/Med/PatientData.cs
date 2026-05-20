using UnityEngine;

[CreateAssetMenu(fileName = "Patient", menuName = "Medical/Patient")]
public class PatientData : ScriptableObject
{
    public string patientName;

    [Header("Correct Symptoms")]
    public string[] correctSymptoms;

    [Header("HEAD")]
    [TextArea] public string headThermometer;
    [TextArea] public string headStethoscope;
    [TextArea] public string headFlashlight;

    [Header("BODY")]
    [TextArea] public string bodyThermometer;
    [TextArea] public string bodyStethoscope;
    [TextArea] public string bodyFlashlight;
}