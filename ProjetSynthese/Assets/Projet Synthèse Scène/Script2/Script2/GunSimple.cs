using UnityEngine;

public class GunSimple : Gun
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource shootSound;

    public override void Shoot()
    {
        if (!canShoot || ammo <= 0) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * 30f, ForceMode.Impulse);

        shootSound?.Play();
        PlayShootAnimation();

        ammo--;
        canShoot = false;
        Invoke(nameof(ResetShoot), shootCooldown);
    }

    private void ResetShoot()
    {
        canShoot = true;
    }
}
