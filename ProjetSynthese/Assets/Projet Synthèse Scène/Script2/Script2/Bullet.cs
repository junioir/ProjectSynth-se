using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum WeaponType
    {
        Basic,
        Advanced,
        Explosive
    }

    [Header("Projectile Settings")]
    [SerializeField] private WeaponType weaponType = WeaponType.Basic;
    [SerializeField] private float baseDamage = 10f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 5f;

    private void Start()
    {
       // Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        float damage = CalculateDamage();

       
        if (other.TryGetComponent(out EnemyMelee meleeEnemy))
        {
            meleeEnemy.ReceiveDamage(damage);
        }
        else if (other.TryGetComponent(out RangedEnemy rangedEnemy))
        {
            rangedEnemy.ReceiveDamage(damage);
        }
        else if (other.TryGetComponent(out ZigZagEnemy zigzagEnemy))
        {
            zigzagEnemy.ReceiveDamage(damage);
        }

        Destroy(gameObject);
    }

    private float CalculateDamage()
    {
        return weaponType switch
        {
            WeaponType.Basic => baseDamage,
            WeaponType.Advanced => baseDamage * 1.5f,
            WeaponType.Explosive => baseDamage * 2f,
            _ => baseDamage
        };
    }
}
