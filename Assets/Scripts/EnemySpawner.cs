using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private int enemyCount = 2;
    public int currentEnemyCount = 0;
    private bool hasSpawned = false;
    private ScoreManager scoreManager;
 

    private void OnTriggerEnter(Collider other)
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        enemyCount = enemyCount + (scoreManager.GetScore() / 400);
        Debug.Log(enemyCount);
        if (other.CompareTag("Player") && currentEnemyCount < enemyCount)
        {
            hasSpawned = true;
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