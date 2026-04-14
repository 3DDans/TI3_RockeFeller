using UnityEngine;
using UnityEngine.UI;

public class SymptomButton : MonoBehaviour
{
    public string symptomName;

    private MedicalGameManager manager;
    private bool selected = false;
    private Image image;

    void Start()
    {
        manager = FindFirstObjectByType<MedicalGameManager>();
        image = GetComponent<Image>();
    }
    public void SetSelected(bool value)
    {
        selected = value;
        image.color = selected ? Color.green : Color.white;
    }

    public void OnClick()
    {
        selected = !selected;

        manager.ToggleSymptom(symptomName);

        image.color = selected ? Color.green : Color.white;

        Debug.Log("Clicou: " + symptomName);
    }
}