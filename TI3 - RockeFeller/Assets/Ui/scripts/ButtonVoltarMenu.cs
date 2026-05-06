using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ButtonVoltarMenu : MonoBehaviour
{
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var botaoVoltarMenu = root.Q<Button>("VoltarMenu");

        botaoVoltarMenu.clicked += () =>
        {
            Debug.Log("CLICOU");
            SceneManager.LoadScene("Fase0");
        };
    }
}