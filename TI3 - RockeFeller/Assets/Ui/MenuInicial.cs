using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuInicialUI : MonoBehaviour
{
    private List<Label> opcoes;
    private int selecao = 0;

    private readonly Color corNormal = Color.gray;
    private readonly Color corSelecao = new Color32(207, 232, 239, 255);

    readonly string[] nomes =
    {
        "CONTINUAR",
        "CONFIGURAÇÕES",
        "SAIR"
    };

    void Start()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;

        var root = GetComponent<UIDocument>().rootVisualElement;

        root.focusable = true;
        root.Focus();

        opcoes = new List<Label>();
        for (int i = 0; i < nomes.Length; i++)
            opcoes.Add(root.Q<Label>($"opcao{i}"));

        root.RegisterCallback<KeyDownEvent>(OnKeyDown);

        AtualizarMenu();
    }

    void OnKeyDown(KeyDownEvent evt)
    {
        switch (evt.keyCode)
        {
            case KeyCode.DownArrow:
                selecao = (selecao + 1) % opcoes.Count;
                AtualizarMenu();
                break;

            case KeyCode.UpArrow:
                selecao = (selecao - 1 + opcoes.Count) % opcoes.Count;
                AtualizarMenu();
                break;

            case KeyCode.Return:
            case KeyCode.KeypadEnter:
                Debug.Log("confirmado");
                EscolherOpcao();
                break;
        }
    }

    void AtualizarMenu()
    {
        for (int i = 0; i < opcoes.Count; i++)
        {
            bool ativo = i == selecao;

            opcoes[i].style.color = ativo ? corSelecao : corNormal;
            opcoes[i].text = ativo
                ? $"> {nomes[i]} <"
                : nomes[i];
        }
    }

    void EscolherOpcao()
    {
        switch (selecao)
        {
            case 0:
                Debug.Log("carregando Fase2...");
                SceneManager.LoadScene("Fase2");
                break;

            case 1:
                Debug.Log("carregando Fase1...");
                SceneManager.LoadScene("Fase1");
                break;

            case 2:
                Debug.Log("saindo...");
                Application.Quit();
                break;
        }
    }
}