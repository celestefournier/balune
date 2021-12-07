using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] UnityEvent onClick;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.DOScale(1.3f, 0.2f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.DOScale(1, 0.2f);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        transform.DOScale(1, 0.1f);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        transform.DOScale(1.3f, 0.1f);
        onClick.Invoke();
    }
}
