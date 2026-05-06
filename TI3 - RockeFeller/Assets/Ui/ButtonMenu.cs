using UnityEngine;

public class ButtonMenu : MonoBehaviour
{
    [SerializeField] private string nomeCena;

    public void Trocar()
    {
        if (string.IsNullOrEmpty(nomeCena))
        {
            Debug.LogWarning("Cena não definida no botão!");
            return;
        }

        //FadeController.Instance.TrocarCena(nomeCena);
    }
}
