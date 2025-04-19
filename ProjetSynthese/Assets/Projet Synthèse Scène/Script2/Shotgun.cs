using UnityEngine;

public class Shotgun : Gun
{
    [Header("Shotgun Specific")]
    public int pellets = 8;
    public float spreadAngle = 15f;
    public GameObject pelletPrefab;

    protected override void PerformShot()
    {
        for (int i = 0; i < pellets; i++)
        {
            Vector3 direction = Camera.main.transform.forward;
            direction = Quaternion.AngleAxis(Random.Range(-spreadAngle, spreadAngle), Camera.main.transform.up) * direction;
            direction = Quaternion.AngleAxis(Random.Range(-spreadAngle, spreadAngle), Camera.main.transform.right) * direction;

            Ray ray = new Ray(Camera.main.transform.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Spawn pellet for visual effect
                if (pelletPrefab != null)
                {
                    GameObject pellet = Instantiate(pelletPrefab, hit.point, Quaternion.identity);
                    Destroy(pellet, 0.5f);
                }

                // Damage logic
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}