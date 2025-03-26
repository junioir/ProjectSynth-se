using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : BaseState
{
    private Transform _target;
    private NavMeshAgent _agent;

    public ChaseState() : base()
    {
    }

    public void init(Transform target, NavMeshAgent agent)
    {
        _target = target;
        _agent = agent;
    }
    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        _agent.SetDestination(_target.position);
    }
}
