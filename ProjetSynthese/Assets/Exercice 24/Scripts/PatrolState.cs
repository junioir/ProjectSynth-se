
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState
{
    private NavMeshAgent _agent;
    private Vector3 _patrolPoint1;
    private Vector3 _patrolPoint2;
    private float _minimumDistance;


    public void init(Vector3 pos1, Vector3 pos2, NavMeshAgent agent, float minimumDistance)
    {
        _patrolPoint1 = pos1;
        _patrolPoint2 = pos2;
        _minimumDistance = minimumDistance;
        _agent = agent;
    }

    public override void Enter()
    {
        _agent.SetDestination(_patrolPoint1);
    }

    public PatrolState() : base()
    {
    }


    public override void Update()
    {
        if((_agent.transform.position - _patrolPoint1).magnitude < _minimumDistance)
        {
            _agent.SetDestination(_patrolPoint2);
        }
        if ((_agent.transform.position - _patrolPoint2).magnitude < _minimumDistance)
        {
            _agent.SetDestination(_patrolPoint1);
        }
    }
    public override void Exit()
    {
    }
}

