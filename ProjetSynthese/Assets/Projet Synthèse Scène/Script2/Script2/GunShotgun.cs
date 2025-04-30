using UnityEngine;

public class GunShotgun : Gun
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int pelletCount = 6;
    [SerializeField] private float spreadAngle = 10f;
    [SerializeField] private AudioSource shootSound;

    public override void Shoot()
    {
        if (!canShoot || ammo <= 0) return;

        for (int i = 0; i < pelletCount; i++)
        {
            Quaternion spread = Quaternion.Euler(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0
            );

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * spread);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 25f, ForceMode.Impulse);
        }

        if (shootSound != null)
            shootSound.Play();

        //shootSound?.Play();
        PlayShootAnimation();
        ammo--;

        canShoot = false;
        Invoke(nameof(ResetShoot), shootCooldown);
    }

    private void ResetShoot() => canShoot = true;
}
