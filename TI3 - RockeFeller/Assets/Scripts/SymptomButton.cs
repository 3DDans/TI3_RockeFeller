using UnityEngine;
using UnityEngine.UI;

public class SymptomButton : MonoBehaviour
{
    public string symptomName;

    private MedicalGameManager manager;
    private bool selected = false;
    private Image image; //cortar pra botar feedback de botão na ui

    void Start()
    {
        manager = FindFirstObjectByType<MedicalGameManager>();
        image = GetComponent<Image>(); //cortar pra botar feedback de botão na ui
    }
    public void SetSelected(bool value)
    {
        selected = value;
        image.color = selected ? Color.green : Color.white; //cortar pra botar feedback de botão na ui
    }

    public void OnClick()
    {
        selected = !selected;

        manager.ToggleSymptom(symptomName);

        image.color = selected ? Color.green : Color.white; //cortar pra botar feedback de botão na ui

        Debug.Log("Clicou: " + symptomName);
    }
}