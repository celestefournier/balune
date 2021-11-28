using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    static int score = 0;
    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
