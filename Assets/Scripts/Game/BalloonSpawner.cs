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
    [SerializeField] GameObject balloonNormal;
    [SerializeField] GameObject balloonTNT;
    [SerializeField] GameObject balloonLose;

    int balloonsCount = 1;
    int roundsToSpawn = 6;
    float balloonSize = 0.8f;
    float spawnRange;

    void Start()
    {
        spawnRange = Camera.main.orthographicSize * Camera.main.aspect - balloonSize;

        List<Balloon> balloons = new List<Balloon>
        {
            balloonNormal.transform.GetChild(0).GetComponent<Balloon>(),
            balloonTNT.transform.GetChild(0).GetComponent<Balloon>(),
            balloonLose.transform.GetChild(0).GetComponent<Balloon>()
        };

        if (!menu)
        {
            scoreManager.onScore.AddListener(score => SpawnBalloon(balloons, score));
            startBalloon.Init(gameController, scoreManager, RemoveBalloon);
            return;
        }

        StartCoroutine("SpawnCoroutine");
    }

    void SpawnBalloon(List<Balloon> spawnBalloons, int score = 0)
    {
        if (!menu && score != 0 && score % roundsToSpawn != 0)
            return;

        float spawnWeight = 0f;

        foreach (var item in spawnBalloons)
            spawnWeight += item.spawnRate;

        float randomWeight = Random.Range(0, spawnWeight);
        float counterWeight = 0f;
        GameObject randomBallon = null;

        foreach (var item in spawnBalloons)
        {
            counterWeight += item.spawnRate;

            if (randomWeight <= counterWeight)
            {
                randomBallon = item.transform.parent.gameObject;
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
    }

    IEnumerator SpawnCoroutine()
    {
        var balloonList = new List<Balloon> { balloonNormal.transform.GetChild(0).GetComponent<Balloon>() };

        while (!GameController.gameOver)
        {
            SpawnBalloon(balloonList);
            yield return new WaitForSeconds(9.5f);
        }
    }

    void RemoveBalloon()
    {
        balloonsCount--;

        if (balloonsCount <= 0)
        {
            var balloonList = new List<Balloon> { balloonNormal.transform.GetChild(0).GetComponent<Balloon>() };
            SpawnBalloon(balloonList);
        }
    }
}
