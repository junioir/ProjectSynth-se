using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public int ammo = 10;
    public int maxAmmo = 10;
    public float shootCooldown = 0.5f;

    protected bool canShoot = true;

    [SerializeField] protected Animator gunAnimator;

    public abstract void Shoot();

    public virtual void Reload()
    {
        ammo = maxAmmo;
    }

    protected void PlayShootAnimation()
    {
        if (gunAnimator != null)
            gunAnimator.SetTrigger("Shoot");
    }
}
