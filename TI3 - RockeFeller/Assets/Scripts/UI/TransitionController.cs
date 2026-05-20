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
        animator.SetTrigger("Open");
    }

    public void FadeOut()
    {
        animator.SetTrigger("Close");
    }

    public void FadeIn()
    {
        animator.SetTrigger("Open");
    }
}
