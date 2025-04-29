using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Common Enemy Settings")]
    [SerializeField] protected float _movementSpeed;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _attackCooldown;
    [SerializeField] protected float _life;
    [SerializeField] protected int _damage;
    protected Transform _player;
    protected bool _canAttack = true;

    protected virtual void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected virtual void Update()
    {
        if (_player == null) return;

        MoveAndAttack();
    }

    protected abstract void MoveAndAttack();

 

    public virtual void ReceiveDamage(float damage)
    {
        _life -= damage;
        if (_life <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
