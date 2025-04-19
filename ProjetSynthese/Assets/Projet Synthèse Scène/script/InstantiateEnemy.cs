using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _enemyCount;
    private void Start()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            Vector3 position = Random.insideUnitSphere;
            CreateEnemy(position);
        }
    }
    private void CreateEnemy(Vector3 position)
    {
        Enemy enemy = Instantiate(_enemy, position, Quaternion.identity);
        Color color = GetRandomcolor();
       // enemy.ChangeColor(color);
    }
    private Color GetRandomcolor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
