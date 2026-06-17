using System.Collections;
using TMPro;
using UnityEngine;

public class AreaNameUI : MonoBehaviour
{
    public static AreaNameUI Instance;

    public GameObject panel;
    public TextMeshProUGUI areaText;

    Coroutine currentRoutine;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowArea(string areaName)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(areaName));
    }

    IEnumerator ShowRoutine(string areaName)
    {
        panel.SetActive(true);

        areaText.text = "";

        foreach (char letter in areaName)
        {
            areaText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);

        panel.SetActive(false);
    }
}