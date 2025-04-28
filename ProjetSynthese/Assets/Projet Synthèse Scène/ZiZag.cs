using System.Collections;
using UnityEngine;

public class ZigZag : Enemy
{
    [SerializeField] private AnimationController _controller;
    [SerializeField] private float _zigzagAmplitude = 2f; // largeur du zigzag
    [SerializeField] private float _zigzagFrequency = 2f; // vitesse du zigzag

    private float _zigzagTimer = 0f;
    private Vector3 _forwardDirection;

    protected override void Start()
    {
        base.Start();
        if (_player != null)
        {
            _forwardDirection = (_player.position - transform.position).normalized;
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void MoveAndAttack()
    {
        if (_player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer > _attackRange)
        {
            _controller.SetIsWalking();
            MoveWithZigzag();
        }
        else
        {
            _controller.SetIsAttacking();
            StopAndAttack();
        }
    }

    private void MoveWithZigzag()
    {
        if (_player == null) return;

        _zigzagTimer += Time.deltaTime * _zigzagFrequency;

        // Mouvement de base vers le joueur
        Vector3 forwardMove = _forwardDirection * _movementSpeed * Time.deltaTime;

        // Déplacement latéral en zigzag
        Vector3 sideMove = transform.right * Mathf.Sin(_zigzagTimer) * _zigzagAmplitude * Time.deltaTime;

        // Mouvement combiné
        transform.position += forwardMove + sideMove;

        // Toujours orienté vers le joueur
        Vector3 lookAtTarget = new Vector3(_player.position.x, transform.position.y, _player.position.z);
        transform.LookAt(lookAtTarget);
    }

    private void StopAndAttack()
    {
        transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));
        if (_canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        _canAttack = false;

        // Exemple : ici on peut simplement infliger des dégâts directement (à adapter si besoin)
        PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(_damage);
        }

        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
}
