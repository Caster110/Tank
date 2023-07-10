using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyUnit;
    private GameObject[] spawnPoints;
    private float staticTimeBtwSpawn = 4.5f;
    private float timerBtwSpawn;

    private System.Random randomizer;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
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
        int randomSpawn = randomizer.Next(9);
        Transform spawnPoint = spawnPoints[randomSpawn].transform;
        Instantiate(enemyUnit, spawnPoint.position, spawnPoint.rotation);
    }
}
