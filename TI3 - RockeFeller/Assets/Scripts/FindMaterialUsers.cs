using UnityEngine;

public class FindMaterialUsers : MonoBehaviour
{
    public Material targetMaterial;

    [ContextMenu("Find Objects")]
    void FindObjects()
    {
        Renderer[] renderers = FindObjectsByType<Renderer>(FindObjectsSortMode.None);

        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.sharedMaterials)
            {
                if (mat == targetMaterial)
                {
                    Debug.Log(rend.gameObject.name, rend.gameObject);
                    break;
                }
            }
        }
    }
}