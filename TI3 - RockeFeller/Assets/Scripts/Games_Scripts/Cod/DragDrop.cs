using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Image image;
    public RectTransform canvasRect;
    private Vector2 startingPoint;
    private CanvasGroup canvasGroup;
    public bool onSlot = false;
    public GameObject correctSlot;
    public GameObject currentSlot;
    public bool isInCorrectSlot = false;

    private void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        startingPoint = rectTransform.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("OnPointerDown");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        if (!onSlot)
        {
            rectTransform.anchoredPosition = startingPoint;
            image.color = ColorUtility.TryParseHtmlString("#0081FF", out var cor) ? cor : Color.white;
        }
        else
        {
            currentSlot = eventData.pointerCurrentRaycast.gameObject;
        }

        canvasGroup.blocksRaycasts = true;
        onSlot = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            rectTransform.anchoredPosition = localPoint;
        }
    }
}
