using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public UnityEvent<int> onScore;

    public static int score = 0;
    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
        onScore.Invoke(score);
    }
}
