using UnityEngine;

public class SpecialEnemy : Enemy
{
    [Header("Special Movement")]
    public float zigzagFrequency = 2f;
    public float zigzagAmplitude = 2f;
    private float zigzagOffset;

    protected override void Start()
    {
        base.Start();
        zigzagOffset = Random.Range(0f, 100f);
    }

    protected override void MoveToExit()
    {
        Vector3 direction = (exitPoint.position - transform.position).normalized;
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized;

        float zigzag = Mathf.Sin((Time.time + zigzagOffset) * zigzagFrequency) * zigzagAmplitude;
        Vector3 targetPosition = exitPoint.position + perpendicular * zigzag;

        agent.SetDestination(targetPosition);
        animator.SetBool("IsRunning", true);
    }
}