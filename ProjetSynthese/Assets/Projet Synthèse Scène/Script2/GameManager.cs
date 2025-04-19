using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int currentWave;
    public int playerHealth;
    public int playerResources;
    public bool gameOver;

    // Add the missing event definition  
    public event Action OnResourcesChanged;

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

    public void AddResources(int amount)
    {
        playerResources += amount;
        OnResourcesChanged?.Invoke(); // Trigger the event when resources change  
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            GameOver(false);
        }
    }

    public void GameOver(bool victory)
    {
        gameOver = true;
        // Additional game over logic here  
    }
}
