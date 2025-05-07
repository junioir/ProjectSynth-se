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
    public float timeBetweenWaves = 15f;

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
    public TextMeshProUGUI enemyCountText;
    public GameObject victoryPanel;

    private int enemiesAlive = 0;
    private bool isWaveInProgress = false;


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
        yield return new WaitForSeconds(timeBetweenWaves);

        currentWave++;
        isWaveInProgress = true;
        Debug.Log(">>> SPAWN DE LA VAGUE " + currentWave);
        if (waveText != null)
            waveText.text = "Vague : " + currentWave;

        SpawnWave(currentWave);
    }


  
    void SpawnWave(int waveNumber)
    {
        enemiesAlive = 0;
        Debug.Log(">>> SPAWN DE LA VAGUE " + waveNumber);
        isWaveInProgress = true;


        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy(meleeEnemyPrefab, meleeSpawnPoint, waveNumber);
            SpawnEnemy(rangedEnemyPrefab, rangedSpawnPoint, waveNumber);
            SpawnEnemy(zigzagEnemyPrefab, zigzagSpawnPoint, waveNumber);
        }
    }


    
    void SpawnEnemy(GameObject prefab, Transform spawnPoint, int wave)
    {
       Vector3 offset = new Vector3( Random.Range(-100f, 100f), 0f, Random.Range(100f, 100f));

        Vector3 spawnPosition = spawnPoint.position + offset;

        GameObject enemy = Instantiate(prefab, spawnPosition, Quaternion.identity);

       
        Enemy baseEnemy = enemy.GetComponent<Enemy>();
        if (baseEnemy != null)
        {
            baseEnemy.SetWaveScaling(wave);
        }

        enemiesAlive++;
        UpdateEnemyCountUI();
    }


    public void EnemyDied()
    {
        enemiesAlive--;
        ResourceManager.Instance.AddResource(40); 
        UpdateEnemyCountUI();
        if (enemiesAlive <= 0 && isWaveInProgress)

        {
            Debug.Log("Tous les ennemis sont morts, prochaine vague !");
            isWaveInProgress = false;
            if (currentWave < totalWaves)
                StartCoroutine(StartNextWave());
            else
                ShowVictory();
        }
        Debug.Log("Enemy mort, restants : " + enemiesAlive);

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
    private void UpdateEnemyCountUI()
    {
        if (enemyCountText != null)
            enemyCountText.text = "Ennemis restants : " + enemiesAlive;
    }

}
