using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class BotaoCenaUI : MonoBehaviour
{
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        Button botao = root.Q<Button>("botao");

        if (botao == null)
        {
            Debug.LogError("Botão não encontrado!");
            return;
        }

        botao.clicked += () =>
        {
            SceneManager.LoadScene("Fase0");
        };
    }
}
