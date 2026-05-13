using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MedicalGameManager : MinigameBase
{
    public PatientData currentPatient;

    public List<string> selectedSymptoms = new List<string>();

    public TextMeshProUGUI feedbackText;

   
    public GameObject medicalUI;
    public SymptomButton[] allButtons;
    public TextMeshProUGUI patientNameText;
    void Start()
    {
        
        patientNameText.text = currentPatient.patientName;
        
    }


    public void SetSymptom(string symptom, bool active)
    {
        if (active)
        {
            if (!selectedSymptoms.Contains(symptom))
                selectedSymptoms.Add(symptom);
        }
        else
        {
            selectedSymptoms.Remove(symptom);
        }

        Debug.Log("Lista atual:");

        foreach (var s in selectedSymptoms)
        {
            Debug.Log(s);
        }
    }

    public void SubmitDiagnosis()
    {
        List<string> correctSelected = new List<string>();
        List<string> wrongSelected = new List<string>();

        foreach (string selected in selectedSymptoms)
        {
            if (currentPatient.correctSymptoms.Contains(selected))
            {
                correctSelected.Add(selected);
            }
            else
            {
                wrongSelected.Add(selected);
            }
        }

        int totalCorrect = currentPatient.correctSymptoms.Length;

        // ? PERFEITO
        if (correctSelected.Count == totalCorrect && wrongSelected.Count == 0)
        {
            feedbackText.text = "Perfect Diagnosis!";
            Debug.Log("ACERTO TOTAL");

            EndMinigame();
            return;
        }

        // ?? PARCIAL
        if (correctSelected.Count > 0)
        {
            feedbackText.text = "Parcial! Review some symptoms.";

            // mantém só os corretos
            selectedSymptoms = new List<string>(correctSelected);

            UpdateButtonsVisual();

            return;
        }

        // ? ERRO TOTAL
        feedbackText.text = "Try Again. You can do it!";

        selectedSymptoms.Clear();

        UpdateButtonsVisual();
    }
    void EndMinigame()
    {
        Invoke(nameof(Finish), 1.5f);
    }

    void Finish()
    {
        medicalUI.SetActive(false);

        CompleteMinigame();
    }

    void UpdateButtonsVisual()
    {
        foreach (var button in allButtons)
        {
            bool isSelected = selectedSymptoms.Contains(button.symptomName);
            button.SetSelected(isSelected);
        }
    }

} 