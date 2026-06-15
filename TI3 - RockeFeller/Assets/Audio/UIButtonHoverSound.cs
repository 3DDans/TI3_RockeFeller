using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioSource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundFXManager.Instance.PlaySFX("btnHover");
    }
}