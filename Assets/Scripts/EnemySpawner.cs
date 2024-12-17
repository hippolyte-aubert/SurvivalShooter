using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    [Range(0.0f, 1.0f)] public float waveSpawnProbability = 0.5f;
    public float spawnRate = 2f;
    public int enemiesPerWave = 5;
    private float _nextSpawnTime;
    private bool _isSpawning = true;
    
    private void Update()
    {
        if (_isSpawning && !GameManager.instance.isGameOver)
        {
            if (Time.time >= _nextSpawnTime)
            {
                SpawnEnemy();
                _nextSpawnTime = Time.time + spawnRate;
            }
        }
    }
    
    private void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        if (Random.value < waveSpawnProbability)
        {
            StartCoroutine(SpawnWave());
        }
    }
    
    private IEnumerator SpawnWave()
    {
        _isSpawning = false;
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
        _isSpawning = true;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
