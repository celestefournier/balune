using System.Collections;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] bool menu;
    [SerializeField] BalloonWeight[] balloons;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameController gameController;
    [SerializeField] int scoreToSpawn;

    float spawnWidth;
    float balloonSize = 0.8f;
    int roundsToSpawn = 6;
    float totalWeight;

    void Start()
    {
        spawnWidth = Camera.main.orthographicSize * Camera.main.aspect - balloonSize;

        foreach (var balloon in balloons)
            totalWeight += balloon.weight;

        if (!menu)
        {
            scoreManager.onScore.AddListener(SpawnBalloon);
            return;
        }

        StartCoroutine("SpawnCoroutine");
    }

    void SpawnBalloon(int score = 0)
    {
        if (!menu && score % roundsToSpawn != 0)
            return;

        float randomWeight = Random.Range(0, totalWeight);
        float counterWeight = 0f;
        GameObject randomBallon = new GameObject();

        foreach (var item in balloons)
        {
            counterWeight += item.weight;

            if (randomWeight <= counterWeight)
            {
                randomBallon = item.balloon;
                break;
            }
        }

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
