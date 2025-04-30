using UnityEngine;

public class GunRocketLauncher : Gun
{
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource shootSound;

    public override void Shoot()
    {
        if (!canShoot || ammo <= 0) return;

        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
        rocket.GetComponent<Rigidbody>().AddForce(firePoint.forward * 30f, ForceMode.Impulse);

        if (shootSound != null)
            shootSound.Play();

        // shootSound?.Play();
        PlayShootAnimation();
ammo--;
        canShoot = false;
        Invoke(nameof(ResetShoot), shootCooldown);
    }

    private void ResetShoot() => canShoot = true;
}
