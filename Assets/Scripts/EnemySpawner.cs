using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 2;
    private int currentEnemyCount = 0;
    private ScoreManager scoreManager;
    public float spawnCooldown = 8f;
    private float lastSpawn = -999f;
    private void Start()
    {
        enemyCount = 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        enemyCount = enemyCount + (scoreManager.GetScore() / 400);
        Debug.Log("Number of Enemies: " + enemyCount);
        Debug.Log("1_Last Spawn: " + lastSpawn);
        if (other.CompareTag("Player") && currentEnemyCount < enemyCount && Time.time >= lastSpawn + spawnCooldown)
        {
            Debug.Log("2_Last Spawn: " + lastSpawn);
            lastSpawn = Time.time;
            for (currentEnemyCount = 0; currentEnemyCount < enemyCount; currentEnemyCount++)
            {
                Vector3 spawnPos = transform.position + new Vector3(
                    Random.Range(-15f, 15f), 
                    0, 
                    Random.Range(-15f, 15f)
                );
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }

        }
    }
}