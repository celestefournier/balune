using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float duration = 0.5f;
    [SerializeField] float strength = 0.2f;
    [SerializeField] int vibrato = 12;
    [SerializeField] float randomness = 90;
    [SerializeField] bool fadeOut = true;

    Camera cam;
    Vector3 startPosition;

    void Start()
    {
        cam = GetComponent<Camera>();
        startPosition = transform.position;
    }

    public void Shake()
    {
        cam.DOShakePosition(duration, strength, vibrato)
            .OnComplete(() =>
            {
                cam.transform.position = startPosition;
            });
    }
}
