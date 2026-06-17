using UnityEngine;
using UnityEngine.UI;

public class DisableGrid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GridLayoutGroup grid;
    private static DisableGrid _instance;
    public static DisableGrid Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }   

    // Update is called once per frame
    public void Disable()
    {
        grid.enabled = false;
    }
}
