using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] UnityEvent onClick;

    bool mouseOver;
    bool pressed;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (!pressed)
        {
            mouseOver = true;
            transform.DOScale(1.3f, 0.2f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData = null)
    {
        mouseOver = false;
        transform.DOScale(1, 0.2f);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        pressed = true;
        transform.DOScale(1, 0.1f);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (mouseOver) transform.DOScale(1.3f, 0.1f);
        pressed = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke();
    }

    void OnDisable()
    {
        OnPointerExit();
    }
}
