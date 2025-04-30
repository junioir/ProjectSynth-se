using System;
using System.Collections;
using UnityEngine;

public class EnemyMelee : Enemy
{
    [SerializeField] private AnimationController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _damageOnCollision = 2;
    [SerializeField] private float _speed = 2f;
   // [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _playerChaseDistance = 5f; // distance pour switch sur joueur

    private Transform _exitPoint;
    private Transform _target; // Vers qui on se déplace

    protected override void Start()
    {
        base.Start();

        GameObject exitObject = GameObject.FindGameObjectWithTag("ExitPoint");
        if (exitObject != null)
        {
            _exitPoint = exitObject.transform;
            _target = _exitPoint;
        }
        else
        {
            Debug.LogError("No ExitPoint found in scene!");
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void MoveAndAttack()
    {
        if (_player == null || _exitPoint == null) return;

        // Choisir cible : joueur si proche sinon sortie
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= _playerChaseDistance)
        {
            _target = _player;
        }
        else
        {
            _target = _exitPoint;
        }

        float distanceToTarget = Vector3.Distance(transform.position, _target.position);

        if (_target == _player && distanceToTarget <= _attackRange)
        {
            _controller.SetIsNotWalking();
            _controller.SetIsAttacking();
            AttackPlayer();
        }
        else
        {
            _controller.SetIsWalking();
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        if (_target == null) return;

        Vector3 direction = (_target.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);

        // Orienter l'ennemi vers la cible
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    }

    private void AttackPlayer()
    {
        // attaque le joueur
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
        else if (other.CompareTag("ExitPoint"))
        {
            // Si atteint ExitPoint, inflige dégât et disparaît
            GameManager.instance.TakeDamage(_damageOnCollision);
            Destroy(gameObject);
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
}
