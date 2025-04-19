using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int meleeEnemies;
        public int rangedEnemies;
        public int specialEnemies;
        public float spawnInterval = 1f;
        public float waveCooldown = 10f;
    }

    public List<Wave> waves;
    public Transform[] spawnPoints;
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public GameObject specialEnemyPrefab;

    private int currentWaveIndex = 0;
    private bool waveInProgress = false;
    private int enemiesRemaining = 0;

    private void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (!waveInProgress && enemiesRemaining == 0 && currentWaveIndex < waves.Count)
        {
            StartCoroutine(StartWaveAfterDelay(waves[currentWaveIndex].waveCooldown));
        }
    }

    private void StartNextWave()
    {
        if (currentWaveIndex >= waves.Count)
        {
            GameManager.Instance.GameOver(true);
            return;
        }

        currentWaveIndex++;
        GameManager.Instance.currentWave = currentWaveIndex;
        UIManager.Instance.UpdateWaveText(currentWaveIndex);

        StartCoroutine(SpawnWave(waves[currentWaveIndex - 1]));
    }

    private IEnumerator StartWaveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartNextWave();
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        waveInProgress = true;
        enemiesRemaining = wave.meleeEnemies + wave.rangedEnemies + wave.specialEnemies;

        for (int i = 0; i < wave.meleeEnemies; i++)
        {
            SpawnEnemy(meleeEnemyPrefab);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        for (int i = 0; i < wave.rangedEnemies; i++)
        {
            SpawnEnemy(rangedEnemyPrefab);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        for (int i = 0; i < wave.specialEnemies; i++)
        {
            SpawnEnemy(specialEnemyPrefab);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        waveInProgress = false;
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void EnemyDied()
    {
        enemiesRemaining--;
    }
}