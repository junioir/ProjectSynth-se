using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] private StateMachine _stateMachine;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _patrolPoint1;
    [SerializeField] private Transform _patrolPoint2;
    [SerializeField] private Transform _target;
    [SerializeField] protected States _state;
    [SerializeField] private float _minimumDistance = 0.5f;

    private States _previousState;
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        if (_previousState != _state) 
        {
            _previousState = _state;
            ChangeState(_state);
        }

    }

    private void ChangeState(States state)
    {
        switch (state)
        {
            case States.Idle:
                IdleState Idle = new IdleState();
                Idle.init(_startPos, _agent);
                _stateMachine.ChangeState(Idle);
                break;
            case States.Patrol:
                PatrolState Patrol = new PatrolState();
                Patrol.init(_patrolPoint1.position, _patrolPoint2.position, _agent, _minimumDistance);
                _stateMachine.ChangeState(Patrol);
                break;
            case States.Pursuit:
                ChaseState Chase = new ChaseState();
                Chase.init(_target, _agent);
                _stateMachine.ChangeState(Chase);
                break;
                default:
                break;
        }
    }
}
