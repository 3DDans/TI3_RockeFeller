using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MedicalGameManager : MonoBehaviour
{
    public PatientData currentPatient;

    public List<string> selectedSymptoms = new List<string>();

    public TextMeshProUGUI feedbackText;

    public NPCInteraction npc;
    public GameObject medicalUI;
    public SymptomButton[] allButtons;
    public TextMeshProUGUI patientNameText;
    void Start()
    {
        patientNameText.text = currentPatient.patientName;
    }

    public void ToggleSymptom(string symptom)
    {
        if (selectedSymptoms.Contains(symptom))
        {
            selectedSymptoms.Remove(symptom);
        }
        else
        {
            selectedSymptoms.Add(symptom);
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
            feedbackText.text = "Diagnóstico perfeito!";
            Debug.Log("ACERTO TOTAL");

            EndMinigame();
            return;
        }

        // ?? PARCIAL
        if (correctSelected.Count > 0)
        {
            feedbackText.text = "Parcial! Revise os sintomas.";

            // mantém só os corretos
            selectedSymptoms = new List<string>(correctSelected);

            UpdateButtonsVisual();

            return;
        }

        // ? ERRO TOTAL
        feedbackText.text = "Tudo incorreto! Tente novamente.";

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

        npc.CompletePuzzle();
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