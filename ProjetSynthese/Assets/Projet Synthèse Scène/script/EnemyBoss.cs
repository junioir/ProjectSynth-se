using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using UnityEngine;
public class EnemyBoss : MonoBehaviour
{
    [SerializeField] private AnimationController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private MeshRenderer _graphics;
   // [SerializeField] private int _damageOncollision = 15;
    [SerializeField] private float _speed;

    private Transform _target;
    private int _despoint;

    void Start()
    {
        if (_waypoints.Length > 0)
        {
            _target = _waypoints[0]; // Initialiser la cible avec le premier waypoint
        }
        else
        {
            Debug.LogError("Aucun waypoint assigné !");
        }
    }
    void Update()
    {
        if (_target == null) return;

        Vector3 dir = _target.position - transform.position;
        transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) < 0.3f)
        {
            _controller.SetIsWalking();
            _despoint = (_despoint + 1) % _waypoints.Length;
            _target = _waypoints[_despoint];

            // Rotation pour orienter l'ennemi vers le prochain waypoint
            Vector3 directionToTarget = _target.position - transform.position;
            if (directionToTarget != Vector3.zero)
            {
                _controller.SetIsAttacking();
            }
        }
    }
}