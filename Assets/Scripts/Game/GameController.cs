using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] CanvasGroup scoreScreen;
    [SerializeField] CanvasGroup gameOverScreen;
    [SerializeField] FadeManager fade;

    public static bool gameOver;
    public static string gameMode = "normal";

    bool fadeIn;
    float fadeInDuration = 0.3f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.transform?.tag == "Balloon")
            {
                hit.transform.GetComponent<Balloon>().Push(hit.point.x - hit.transform.position.x);
                scoreManager.AddScore();
            }
        }
    }

    public void GameOver()
    {
        StartCoroutine("GameOverDelay");
    }

    IEnumerator GameOverDelay()
    {
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(1);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(scoreScreen.DOFade(0, 0.3f));
        sequence.AppendCallback(() => scoreScreen.gameObject.SetActive(false));
        sequence.AppendCallback(() => gameOverScreen.gameObject.SetActive(true));
        sequence.Append(gameOverScreen.DOFade(0, 0.3f).From());
    }

    public void Retry()
    {
        if (!fadeIn)
        {
            fadeIn = true;
            fade.FadeIn(() => {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1;
            }, fadeInDuration);
        }
    }

    public void Menu()
    {
        if (!fadeIn)
        {
            fadeIn = true;
            fade.FadeIn(() => {
                SceneManager.LoadScene("Menu");
                Time.timeScale = 1;
            }, fadeInDuration);
        }
    }
}
