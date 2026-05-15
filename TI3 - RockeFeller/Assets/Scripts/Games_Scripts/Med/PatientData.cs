using UnityEngine;

[CreateAssetMenu(fileName = "Patient", menuName = "Medical/Patient")]
public class PatientData : ScriptableObject
{
    public string patientName;

    [Header("Sintomas corretos")]
    public string[] correctSymptoms;

    [Header("Dicas por área")]
    [TextArea] public string headInfo;
    [TextArea] public string throatInfo;
    [TextArea] public string bodyInfo;
}