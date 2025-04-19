using UnityEngine;

public class Pistol : Gun
{
    [Header("Pistol Specific")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 50f;

    protected override void PerformShot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Spawn bullet at hit point for visual effect
            if (bulletPrefab != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                bullet.transform.LookAt(hit.point);
                bullet.GetComponent<Rigidbody>().velocity = (hit.point - bulletSpawn.position).normalized * bulletSpeed;
                Destroy(bullet, 2f);
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