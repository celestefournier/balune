using System.Collections;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] bool spawnByTime;
    [SerializeField] GameObject balloonPrefab;
    [SerializeField] GameObject balloonTNTPrefab;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameController gameController;
    [SerializeField] int scoreToSpawn;

    float spawnWidth;
    float balloonSize = 0.8f;
    int roundsToSpawn = 6;

    void Start()
    {
        spawnWidth = Camera.main.orthographicSize * Camera.main.aspect - balloonSize;

        if (spawnByTime)
        {
            StartCoroutine("SpawnCoroutine");
        }
        else
        {
            scoreManager.onScore.AddListener(SpawnBalloon);
        }
    }

    void SpawnBalloon(int score = 0)
    {
        if (!spawnByTime)
        {
            if (score % roundsToSpawn != 0) return;
        }

        GameObject[] ballons = { balloonPrefab, balloonTNTPrefab };
        var randomBallon = ballons[Random.Range(0, ballons.Length)];

        float randomX = Random.Range(-spawnWidth, spawnWidth);
        Vector2 randomPosition = new Vector2(randomX, transform.position.y);
        float randomZ = Random.Range(-180, 180);
        Quaternion randomRotation = Quaternion.Euler(0, 0, randomZ);

        GameObject balloon = Instantiate(randomBallon, randomPosition, Quaternion.identity, transform);
        balloon.transform.GetChild(0).localRotation = randomRotation;
        balloon.transform.GetChild(0).GetComponent<Rigidbody2D>().angularVelocity = randomZ;
        balloon.transform.GetChild(0).GetComponent<Balloon>().Init(gameController, scoreManager);
    }

    IEnumerator SpawnCoroutine()
    {
        while (!GameController.gameOver)
        {
            SpawnBalloon();
            yield return new WaitForSeconds(9.5f);
        }
    }
}
