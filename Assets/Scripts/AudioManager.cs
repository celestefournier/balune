using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] float fadeInSpeed;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        audioSource.volume = 0;

        while (audioSource.volume < 1)
        {
            audioSource.volume += fadeInSpeed;
            yield return new WaitForSeconds(0.1f);
        }

        audioSource.volume = 1;
    }
}
