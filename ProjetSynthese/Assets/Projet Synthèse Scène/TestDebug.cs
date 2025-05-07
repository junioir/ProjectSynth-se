using UnityEngine;

public class TestDebug : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();

            if (enemies.Length == 0)
            {
                Debug.Log("Aucun ennemi � tuer.");
                return;
            }

            foreach (var enemy in enemies)
            {
                enemy.ReceiveDamage(9999f);
            }

            Debug.Log("Touche K press�e � Tous les ennemis �limin�s !");
        }
    }
}
