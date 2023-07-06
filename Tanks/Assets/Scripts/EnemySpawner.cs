using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyUnit;
    private GameObject[] spawnPoints;
    private float staticTimeBtwSpawn;
    private float timerBtwSpawn;
    private System.Random randomizer;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        staticTimeBtwSpawn = 4.5f;
        timerBtwSpawn = staticTimeBtwSpawn;
        randomizer = new System.Random();
    }

    void Update()
    {
        timerBtwSpawn -= Time.deltaTime;
        if(timerBtwSpawn <= 0)
        {
            SpawnEnemy();
            timerBtwSpawn = staticTimeBtwSpawn;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 rotationOfTank = new Vector3(0, 0, 0);
        int randomSpawn = randomizer.Next(7);
        switch (randomSpawn)
        {
            case 0:
            case 1:
            case 9:
                rotationOfTank = new Vector3(0, 0, -90);
                break;
            case 4:
            case 5:
            case 6:
                rotationOfTank = new Vector3(0, 0, 90);
                break;
            case 2:
            case 3:
                rotationOfTank = new Vector3(0, 0, 180);
                break;
            case 7:
            case 8:
                rotationOfTank = new Vector3(0, 0, 0);
                break;
        }
        Instantiate(enemyUnit, spawnPoints[randomSpawn].transform.position, Quaternion.Euler(rotationOfTank));
    }
}
