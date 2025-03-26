
using UnityEngine;
using UnityEngine.AI;

public class IdleState : BaseState
{
    private NavMeshAgent _agent;
    private Vector3 _origine;

    public void init(Vector3 origine, NavMeshAgent agent) 
    {
        _agent = agent;
        _origine = origine;
    }

    public IdleState() : base()
    {
    }

    public override void Enter()
    {
        _agent.SetDestination(_origine);
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}

