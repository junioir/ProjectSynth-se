using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [Header("Vagues")]
    public int currentWave = 0;
    public int totalWaves = 3;
    public float timeBetweenWaves = 5f;

    [Header("Spawn Points")]
    public Transform meleeSpawnPoint;
    public Transform rangedSpawnPoint;
    public Transform zigzagSpawnPoint;

    [Header("Enemy Prefabs")]
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public GameObject zigzagEnemyPrefab;

    [Header("UI")]
    public TextMeshProUGUI waveText;
    public GameObject victoryPanel;

    private int enemiesAlive = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        currentWave++;
        if (waveText != null)
            waveText.text = "Vague : " + currentWave;

        yield return new WaitForSeconds(timeBetweenWaves);

        SpawnWave(currentWave);
    }

    

    void SpawnWave(int waveNumber)
    {
        Debug.Log("SPAWN WAVE " + waveNumber);
        enemiesAlive = 0;

        for (int i = 0; i < waveNumber; i++) // instancier X ennemis de chaque type selon la vague
        {
            SpawnEnemy(meleeEnemyPrefab, meleeSpawnPoint, waveNumber);
            SpawnEnemy(rangedEnemyPrefab, rangedSpawnPoint, waveNumber);
            SpawnEnemy(zigzagEnemyPrefab, zigzagSpawnPoint, waveNumber);
        }
    }


    void SpawnEnemy(GameObject prefab, Transform spawnPoint, int wave)
    {
        GameObject enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        Enemy baseEnemy = enemy.GetComponent<Enemy>();
        if (baseEnemy != null)
        {
            baseEnemy.SetWaveScaling(wave);
        }
        enemiesAlive++;
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        ResourceManager.Instance.AddResource(10); // Donne 10 ressources

        if (enemiesAlive <= 0)
        {
            if (currentWave < totalWaves)
                StartCoroutine(StartNextWave());
            else
                ShowVictory();
        }
    }

    void ShowVictory()
    {
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
