using System.Collections;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private AnimationController _controller;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _playerChaseDistance = 8f; // distance pour changer de cible vers le joueur

    private Transform _exitPoint;
    private Transform _target; // vers qui aller

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

        // Choisir la cible : joueur si proche, sinon sortie
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
            _controller.SetIsAttacking();
            StopAndAttack();
        }
        else
        {
            _controller.SetIsWalking();
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        if (_target == null) return;

        Vector3 direction = (_target.position - transform.position).normalized;
        transform.position += direction * _movementSpeed * Time.deltaTime;

        // Orienter vers la cible
        transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));
    }

    private void StopAndAttack()
    {
        if (_target == null) return;

        transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));

        if (_canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        _canAttack = false;

        if (_target == _player)
        {
            // Tirer sur le joueur
            GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
            Projectile proj = projectile.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.SetDamage(_damage);
                proj.SetTarget(_player.position);
            }
            if (_audioSource != null)
            {
                _audioSource.Play();
            }
        }
        else if (_target == _exitPoint)
        {
            // Si à l'ExitPoint, infliger des dégâts directement et détruire l'ennemi
            //GameManager.instance.TakeDamage(_damage);
            WaveManager.Instance.EnemyDied();
            Destroy(gameObject);
            yield break; // important pour stopper la coroutine ici
        }

        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    public override void ReceiveDamage(float damage)
    {
        _life -= damage;
        if (_life <= 0)
        {
            Die();
        }
    }
}
