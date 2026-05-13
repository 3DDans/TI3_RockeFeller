using UnityEngine;
using UnityEngine.UI;

public class SymptomButton : MonoBehaviour
{
    public string symptomName;

    private MedicalGameManager manager;
    private Toggle toggle;

    void Start()
    {
        manager = FindFirstObjectByType<MedicalGameManager>();

        toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        manager.SetSymptom(symptomName, isOn);
    }

    public void SetSelected(bool value)
    {
        toggle.isOn = value;
    }
}