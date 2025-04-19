using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Wave Info")]
    public Text waveText;

    [Header("Player Stats")]
    public Slider healthSlider;
    public Text resourceText;

    [Header("Weapon Info")]
    public Text ammoText;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public Text gameOverText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = "Wave: " + wave;
    }

    public void UpdateHealthBar(int health)
    {
        healthSlider.value = health;
    }

    public void UpdateResourceText(int resources)
    {
        resourceText.text = "Resources: " + resources;
    }

    public void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        ammoText.text = currentAmmo + " / " + maxAmmo;
    }

    public void ShowGameOverScreen(bool victory)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = victory ? "Victory!" : "Defeat!";
    }
}