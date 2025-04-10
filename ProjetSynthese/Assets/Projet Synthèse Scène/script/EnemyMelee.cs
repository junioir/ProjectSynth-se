using System;
using System.Collections;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private AnimationController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _damageOnCollision = 2;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _attackRange = 2f;

    private Transform _player;
    //private bool _isAttacking;
    private const bool V = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (_player == null)
        {
            Debug.LogError("Aucun joueur trouvé dans la scène !");
        }
    }

    void Update()
    {
        if (_player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= _attackRange)
        {
            //_isAttacking = true;
            _controller.SetIsNotWalking();
            _controller.SetIsAttacking();
           // PlaySound(_AttackSound);
            AttackPlayer();
        }
        else
        {
            //_isAttacking = V;
            _controller.SetIsWalking();
           // PlaySound(_WalkSound);
            FollowPlayer();
        }
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
   
}
