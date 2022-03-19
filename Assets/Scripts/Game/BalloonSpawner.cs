using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] bool menu;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameController gameController;
    [SerializeField] int scoreToSpawn;
    [SerializeField] Balloon startBalloon;

    [Header("Balloons")]
    [SerializeField] BalloonWeight balloonNormal;
    [SerializeField] BalloonWeight balloonTNT;
    [SerializeField] BalloonWeight balloonLose;

    int balloonsCount = 1;
    int roundsToSpawn = 6;
    float balloonSize = 0.8f;
    float spawnRange;

    void Start()
    {
        spawnRange = Camera.main.orthographicSize * Camera.main.aspect - balloonSize;

        if (menu)
        {
            StartCoroutine("SpawnCoroutine");
            return;
        }

        List<BalloonWeight> balloons;

        if (GameController.gameMode == "playground")
        {
            balloons = new List<BalloonWeight> { balloonNormal, balloonTNT };
            roundsToSpawn = 2;
        }
        else
            balloons = new List<BalloonWeight> { balloonNormal, balloonTNT, balloonLose };

        scoreManager.onScore.AddListener(score => SpawnBalloon(balloons, score));
        startBalloon.Init(gameController, scoreManager, RemoveBalloon);
        return;
    }

    void SpawnBalloon(List<BalloonWeight> balloons, int score = 0)
    {
        if (!menu && score != 0 && score % roundsToSpawn != 0)
            return;

        float spawnWeight = 0f;

        foreach (var item in balloons)
            spawnWeight += item.weight;

        float randomWeight = Random.Range(0, spawnWeight);
        float counterWeight = 0f;
        GameObject randomBallon = null;

        foreach (var item in balloons)
        {
            counterWeight += item.weight;

            if (randomWeight <= counterWeight)
            {
                randomBallon = item.balloon;
                break;
            }
        }

        float randomX = Random.Range(-spawnRange, spawnRange);
        Vector2 randomPosition = new Vector2(randomX, transform.position.y);
        float randomZ = Random.Range(-180, 180);
        Quaternion randomRotation = Quaternion.Euler(0, 0, randomZ);

        GameObject balloon = Instantiate(randomBallon, randomPosition, Quaternion.identity, transform);
        balloon.transform.GetChild(0).localRotation = randomRotation;
        balloon.transform.GetChild(0).GetComponent<Rigidbody2D>().angularVelocity = randomZ;
        balloon.transform.GetChild(0).GetComponent<Balloon>().Init(gameController, scoreManager, RemoveBalloon);

        balloonsCount++;
        CheckToSpawnTNT();
    }

    IEnumerator SpawnCoroutine()
    {
        var balloonList = new List<BalloonWeight> { balloonNormal };

        while (!GameController.gameOver)
        {
            SpawnBalloon(balloonList);
            yield return new WaitForSeconds(9.5f);
        }
    }

    void RemoveBalloon()
    {
        balloonsCount--;
        CheckToSpawnTNT();

        if (balloonsCount <= 0)
        {
            var balloonList = new List<BalloonWeight> { balloonNormal };
            SpawnBalloon(balloonList);
        }
    }

    void CheckToSpawnTNT()
    {
        float maxBalloons = 6;
        float weight = 1;

        if (balloonsCount > maxBalloons)
        {
            float incrementalWeight = 3;
            float spawnRate = (balloonsCount - maxBalloons) * incrementalWeight + weight;

            balloonTNT.weight = spawnRate;
        }
        else
        {
            balloonTNT.weight = weight;
        }
    }
}

[System.Serializable]
public class BalloonWeight
{
    public GameObject balloon;
    public float weight;
}
