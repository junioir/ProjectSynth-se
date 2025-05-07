using System;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources Instance;

    public event Action OnResourceChanged;

    [SerializeField] private int _currentResources = 100;

    public int CurrentResources => _currentResources;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Plus d'une instance de PlayerResources détectée !");
            return;
        }
        Instance = this;
    }

    public void AddResources(int amount)
    {
        _currentResources += amount;
        OnResourceChanged?.Invoke();
    }

    public bool SpendResources(int cost)
    {
        if (_currentResources < cost) return false;

        _currentResources -= cost;
        OnResourceChanged?.Invoke();
        return true;
    }
}
