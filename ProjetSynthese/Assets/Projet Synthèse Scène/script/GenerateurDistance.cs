using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateurDistance : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnInterval = 8f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, _spawnInterval);
    }
    private void SpawnEnemy()
    {
        if (_enemyPrefab != null)
        {
            // Instancie un ennemi à la position actuelle du spawner
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab de l'ennemi non assigné !");
        }
    }
}
