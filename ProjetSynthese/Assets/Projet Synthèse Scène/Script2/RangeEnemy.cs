using UnityEngine;

public class RangeEnemy : Enemy
{
    [Header("Ranged Specific")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;

    protected override void Attack()
    {
        base.Attack();

        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * projectileSpeed, ForceMode.Impulse);

            Destroy(projectile, 3f);
        }
    }
}