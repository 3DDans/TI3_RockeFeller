using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyTime = 3f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}