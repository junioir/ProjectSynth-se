using UnityEngine;

public class TestDebug : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (var enemy in enemies)
            {
                enemy.ReceiveDamage(9999f); // one-shot kill
            }
        }
    }
}
