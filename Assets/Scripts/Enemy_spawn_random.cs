using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies;
    public float spawnDelay;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy()
    {
        // Get a random position within the scene boundaries
        Vector3 spawnPosition = new Vector3(Random.Range(-3f, 3f), Random.Range(-5f, 5f), 0f);

        // Instantiate the enemy prefab at the random position
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Optionally, you can set up the enemy's properties or add it to a list for future reference
        // For example, you might want to set the enemy's target or add it to a list of spawned enemies
        // enemy.GetComponent<Enemy>().SetTarget(someTarget);
    }
}