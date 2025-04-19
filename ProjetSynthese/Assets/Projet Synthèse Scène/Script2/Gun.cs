using UnityEngine;
using System.Collections;

public abstract class Gun : MonoBehaviour
{
    [Header("Base Gun Stats")]
    public string gunName;
    public int damage = 10;
    public float fireRate = 0.5f;
    public int maxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 2f;
    public int cost = 200;

    [Header("Recoil")]
    public float recoilAmount = 0.1f;
    public float recoilRecoverySpeed = 5f;

    [Header("References")]
    public ParticleSystem muzzleFlash;
    public AudioSource shootSound;
    public Animator animator;

    protected float nextFireTime;
    protected bool isReloading;
    protected Vector3 originalPosition;

    protected virtual void Start()
    {
        currentAmmo = maxAmmo;
        originalPosition = transform.localPosition;
        UIManager.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
    }

    protected virtual void Update()
    {
        // Recoil recovery
        if (transform.localPosition != originalPosition)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * recoilRecoverySpeed);
        }

        // Reload input
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    public virtual void Shoot()
    {
        if (Time.time < nextFireTime || isReloading) return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        currentAmmo--;
        nextFireTime = Time.time + fireRate;

        // Visual effects
        if (muzzleFlash != null) muzzleFlash.Play();
        if (shootSound != null) shootSound.Play();
        if (animator != null) animator.SetTrigger("Shoot");

        // Recoil
        transform.localPosition -= Vector3.forward * recoilAmount;

        // Update UI
        UIManager.Instance.UpdateAmmoText(currentAmmo, maxAmmo);

        // Actual shooting logic
        PerformShot();
    }

    protected abstract void PerformShot();

    protected virtual IEnumerator Reload()
    {
        isReloading = true;
        if (animator != null) animator.SetTrigger("Reload");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        UIManager.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
    }

    public virtual void OnSelect()
    {
        UIManager.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
    }
}