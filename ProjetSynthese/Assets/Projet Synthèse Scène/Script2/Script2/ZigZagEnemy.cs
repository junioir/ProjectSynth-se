using System.Collections;
using UnityEngine;

public class ZigZagEnemy : Enemy
{
    [SerializeField] private AnimationController _controller;
    [SerializeField] private float _zigzagAmplitude = 15f; // degrés de zigzag
    [SerializeField] private float _zigzagFrequency = 2f;  // fréquence
    [SerializeField] private float _playerChaseDistance = 5f; // distance pour attaquer le joueur
    //[SerializeField] private int _damage = 10;

    private float _zigzagTimer = 0f;
    private Transform _exitPoint;
    private Transform _target; // vers où aller

    protected override void Start()
    {
        base.Start();

        GameObject exitObject = GameObject.FindGameObjectWithTag("ExitPoint");
        if (exitObject != null)
        {
            _exitPoint = exitObject.transform;
            _target = _exitPoint; // par défaut on vise la sortie
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
        if (_exitPoint == null || _player == null) return;

        // Choisir la cible : joueur proche ou sortie
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= _playerChaseDistance)
        {
            _target = _player; // poursuivre joueur si proche
        }
        else
        {
            _target = _exitPoint; // sinon continuer vers la sortie
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
            MoveWithZigzag();
        }
    }

    private void MoveWithZigzag()
    {
        if (_target == null) return;

        // Avancer
        transform.position += transform.forward * _movementSpeed * Time.deltaTime;

        // Direction de base
        Vector3 direction = (_target.position - transform.position).normalized;
        direction.y = 0f;

        // Oscillation
        _zigzagTimer += Time.deltaTime * _zigzagFrequency;
        float angleOffset = Mathf.Sin(_zigzagTimer) * _zigzagAmplitude;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation *= Quaternion.Euler(0, angleOffset, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    private void StopAndAttack()
    {
        if (_target == null) return;

        // Fixer la rotation vers la cible
        Vector3 lookTarget = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        transform.LookAt(lookTarget);

        if (_canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        _canAttack = false;

        if (_target.CompareTag("Player"))
        {
            PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_damage);
            }
        }
        else if (_target.CompareTag("ExitPoint"))
        {
           GameManager.instance.TakeDamage(_damage); // perdre vie si atteint la sortie
           Destroy(gameObject); // L'ennemi disparait après avoir touché la sortie
            yield break; // arrêter immédiatement la coroutine pour éviter de réactiver _canAttack
        }

        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
}
