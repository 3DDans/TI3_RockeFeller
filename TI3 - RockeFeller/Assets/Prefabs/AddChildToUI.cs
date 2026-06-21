using UnityEngine;

public class AddChildObjects : MonoBehaviour
{
    public GameObject childTemplate;

    [ContextMenu("Add Childs")]
    void AddChilds()
    {
        foreach (Transform parent in transform)
        {
            GameObject child = Instantiate(childTemplate, parent);

            child.transform.localPosition = Vector3.zero;
            child.transform.localRotation = Quaternion.identity;
            child.transform.localScale = Vector3.one;
        }
    }
}