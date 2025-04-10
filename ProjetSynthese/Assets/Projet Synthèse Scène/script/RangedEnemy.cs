using System.Collections;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private AnimationController _controller;
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private float _attackRange = 10f;
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private float _life = 50f;
    [SerializeField] private int _damage = 10;

    private Transform _player;
    private bool _canAttack = true;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (_player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer > _attackRange)
        {
            _controller.SetIsWalking();
          //  PlaySound(_WalkSound);
            MoveTowardsPlayer();
        }
        else
        {

            StopAndAttack();
            _controller.SetIsAttacking();
           // PlaySound(_AttackSound);
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        transform.position += direction * _movementSpeed * Time.deltaTime;
        // Orienter l'ennemi vers le joueur
        transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));
    }
    private void StopAndAttack()
    {
        // Orienter l'ennemi vers le joueur
        transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));
        if (_canAttack)
        {
            StartCoroutine(Attack());
        }
    }
    private IEnumerator Attack()
    {
        _canAttack = false;
        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.SetDamage(_damage);
            proj.SetTarget(_player.position);
        }
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
    public void ReceiveDamage(float damage)
    {
        _life -= damage;
        if (_life <= 0)
        {
            Destroy(gameObject);
        }
    }
  
}
