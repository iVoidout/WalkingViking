using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 2;
    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            hasSpawned = true;
            for (int i = 0; i < enemyCount; i++)
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