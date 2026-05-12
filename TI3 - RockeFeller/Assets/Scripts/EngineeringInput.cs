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

            RoverPart part = hit.collider.GetComponentInParent<RoverPart>();

            if (part != null)
            {
                gameManager.SelectPart(part);
                return;
            }

            RoverSlot slot = hit.collider.GetComponent<RoverSlot>();

            if (slot != null)
            {
                gameManager.TryPlace(slot);
            }
        }
    }
}