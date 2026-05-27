using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    [System.Serializable]
    public class VFX
    {
        public string name;
        public GameObject prefab;
    }

    public List<VFX> vfxList = new List<VFX>();

    private Dictionary<string, GameObject> vfxDictionary;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        vfxDictionary = new Dictionary<string, GameObject>();

        foreach (VFX vfx in vfxList)
        {
            if (!vfxDictionary.ContainsKey(vfx.name))
            {
                vfxDictionary.Add(vfx.name, vfx.prefab);
            }
        }
    }

    public void PlayVFX(string vfxName, Vector3 position)
    {
        if (vfxDictionary.TryGetValue(vfxName, out GameObject prefab))
        {
            Instantiate(prefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("VFX não encontrado: " + vfxName);
        }
    }
}