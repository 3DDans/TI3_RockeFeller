using UnityEngine;

public class EngineeringInput : MonoBehaviour
{
    private Camera cam;
    public EngineeringGameManager gameManager;

    public LayerMask interactLayer;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryClick();
        }
    }

    void TryClick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, interactLayer))
        {
            Debug.Log("Acertou: " + hit.collider.name);

            BuildPart part = hit.collider.GetComponentInParent<BuildPart>();

            if (part != null)
            {
                gameManager.SelectPart(part);
                return;
            }

            BuildSlot slot = hit.collider.GetComponent<BuildSlot>();

            if (slot != null)
            {
                gameManager.TryPlace(slot);
            }
        }
    }
}