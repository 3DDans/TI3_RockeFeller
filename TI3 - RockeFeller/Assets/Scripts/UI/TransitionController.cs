using UnityEngine;

public class TransitionController : MonoBehaviour
{
    private Animator animator;
    public static TransitionController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Open", true);
    }

    public void FadeOut()
    {
        animator.SetBool("Open", false);
    }

    public void FadeIn()
    {
        animator.SetBool("Open", true);
    }
}
