using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateurEnemyMelee : MonoBehaviour
{
    [SerializeField] private int _enemyCount;
    public EnemyMelee _enemy;

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
        EnemyMelee enemy = Instantiate(_enemy, position, Quaternion.identity);
        
    }
   
}
