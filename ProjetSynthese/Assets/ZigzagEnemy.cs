using UnityEngine;

public class ZigzagEnemy : Enemy
{
    public float zigzagAmplitude = 2f;
    public float zigzagFrequency = 2f;

    private Vector3 startPosition;
    private float startTime;

    protected override void Start()
    {
        base.Start();
        startPosition = transform.position;
        startTime = Time.time;
    }

    protected override void MoveToExit()
    {
        // Direction de base vers la sortie
        Vector3 direction = (exitPoint.position - transform.position).normalized;

        // Calcul d’un vecteur perpendiculaire pour faire le zigzag
        Vector3 perp = Vector3.Cross(direction, Vector3.up).normalized;

        // Calcul du décalage sinusoïdal
        float offset = Mathf.Sin((Time.time - startTime) * zigzagFrequency) * zigzagAmplitude;

        // Nouvelle direction avec zigzag
        Vector3 zigzagTarget = transform.position + direction + perp * offset;

        agent.SetDestination(zigzagTarget);
        animator.SetBool("IsRunning", true);
    }
}
