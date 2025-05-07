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
                Debug.Log("Aucun ennemi à tuer.");
                return;
            }

            foreach (var enemy in enemies)
            {
                enemy.ReceiveDamage(9999f);
            }

            Debug.Log("Touche K pressée – Tous les ennemis éliminés !");
        }
    }
}
