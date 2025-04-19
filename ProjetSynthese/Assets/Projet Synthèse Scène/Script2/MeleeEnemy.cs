using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Melee Specific")]
    public float chargeSpeed = 5f;

    protected override void ChasePlayer()
    {
        agent.speed = chargeSpeed;
        base.ChasePlayer();
    }

    protected override void Attack()
    {
        base.Attack();
        // Additional melee effects
    }
}