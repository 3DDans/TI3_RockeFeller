using UnityEngine;

public class AutomaticDoorsController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            AbrirPorta();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FecharPorta();
        }
    }

    void AbrirPorta()
    {
        animator.SetBool("Open", true);
        Debug.Log("Abrir porta automatica");
        SoundFXManager.Instance.PlaySFX("Door");
    }

    public void FecharPorta()
    {
        animator.SetBool("Open", false);
        Debug.Log("Fechou porta automatica");
        SoundFXManager.Instance.PlaySFX("Door");
    }
}
