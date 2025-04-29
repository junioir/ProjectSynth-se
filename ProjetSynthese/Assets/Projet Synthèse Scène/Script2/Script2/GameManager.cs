using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player Stats")]
    [SerializeField] private int playerLife = 100;

    [Header("UI References")]
    [SerializeField] private Text lifeText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private bool gameEnded = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // Optionnel : garde le manager entre les scènes
    }

    private void Start()
    {
        UpdateLifeUI();
    }

    public void TakeDamage(int amount)
    {
        if (gameEnded) return;

        playerLife -= amount;
        UpdateLifeUI();

        if (playerLife <= 0)
        {
            playerLife = 0;
            LoseGame();
        }
    }

    private void UpdateLifeUI()
    {
        if (lifeText != null)
        {
            lifeText.text = "Life: " + playerLife.ToString();
        }
    }

    public void WinGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        if (winScreen != null) winScreen.SetActive(true);

        Invoke(nameof(ReloadScene), 5f);
    }

    private void LoseGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        if (loseScreen != null) loseScreen.SetActive(true);

        Invoke(nameof(ReloadScene), 5f);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
