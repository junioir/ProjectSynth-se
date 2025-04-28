using System;
using System.Collections;
using UnityEngine;

public class EnemyMelee : Enemy
{
    [SerializeField] private AnimationController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _damageOnCollision = 2;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _attackRange = 2f;

    //private Transform _player;
    //private bool _isAttacking;
    private const bool V = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void FollowPlayer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
        // Orienter l'ennemi vers le joueur
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    }

    private void AttackPlayer()
    {
        // Logique d'attaque (par exemple, infliger des dégâts)
        PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(_damageOnCollision);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_damageOnCollision);
            }
        }
    }
    internal void ChangeColor(Color color)
    {
        throw new NotImplementedException();
    }
    public void ResetAttack()

    {
        _animator.SetBool("IsAttacking", false);
    }

    protected override void MoveAndAttack()
    {
        if (_player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= _attackRange)
        {
            _controller.SetIsNotWalking();
            _controller.SetIsAttacking();
            AttackPlayer();
        }
        else
        {
            _controller.SetIsWalking();
            FollowPlayer();
        }
    }

}
