using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] GameObject balloonPrefab;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] int scoreToSpawn;

    float spawnWidth;
    float balloonSize = 1;

    void Start()
    {
        spawnWidth = Camera.main.orthographicSize * Camera.main.aspect - balloonSize;
        scoreManager.onScore.AddListener(SpawnBalloon);
    }

    void SpawnBalloon(int score)
    {
        if (score % scoreToSpawn != 0) return;

        float randomX = Random.Range(-spawnWidth, spawnWidth);
        Vector2 randomPosition = new Vector2(randomX, transform.position.y);
        float randomZ = Random.Range(-180, 180);
        Quaternion randomRotation = Quaternion.Euler(0, 0, randomZ);

        GameObject balloon = Instantiate(balloonPrefab, randomPosition, Quaternion.identity, transform);
        balloon.transform.GetChild(0).localRotation = randomRotation;
        balloon.transform.GetChild(0).GetComponent<Rigidbody2D>().angularVelocity = randomZ;
    }
}
