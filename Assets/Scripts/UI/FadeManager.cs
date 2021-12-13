using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] bool autoplay;
    [SerializeField] float defaultDuration;

    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        if (autoplay) FadeOut();
    }

    public void FadeIn(TweenCallback callback = null, float duration = 0)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        image.DOFade(1, duration == 0 ? defaultDuration : duration).SetEase(Ease.Linear).OnComplete(callback);
    }

    public void FadeOut(TweenCallback callback = null, float duration = 0)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        image.DOFade(0, duration == 0 ? defaultDuration : duration).SetEase(Ease.Linear).OnComplete(callback);
    }
}
