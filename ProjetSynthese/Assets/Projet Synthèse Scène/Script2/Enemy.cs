using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [Header("Base Enemy Stats")]
    public int maxHealth = 100;
    public int damage = 10;
    public float moveSpeed = 3f;
    public int resourceReward = 25;

    [Header("Attack Settings")]
    public float attackRange = 2f;
    public float attackRate = 1f;

    protected int currentHealth;
    protected Transform player;
    protected Transform exitPoint;
    protected NavMeshAgent agent;
    protected float nextAttackTime;
    protected Animator animator;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        exitPoint = GameObject.FindGameObjectWithTag("Exit").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = moveSpeed;
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.gameOver) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float distanceToExit = Vector3.Distance(transform.position, exitPoint.position);

        if (distanceToPlayer < distanceToExit && distanceToPlayer < 15f)
        {
            ChasePlayer();
        }
        else
        {
            MoveToExit();
        }
    }

    protected virtual void MoveToExit()
    {
        agent.SetDestination(exitPoint.position);
        animator.SetBool("IsRunning", true);
    }

    protected virtual void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("IsRunning", true);

        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    protected virtual void Attack()
    {
        animator.SetTrigger("Attack");
        // Damage logic in animation event
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        animator.SetTrigger("Die");
        GameManager.Instance.AddResources(resourceReward);
        Destroy(gameObject, 2f);
        agent.isStopped = true;
        enabled = false;
    }

    // Animation event
    public void DealDamage()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            GameManager.Instance.TakeDamage(damage);
        }
    }
}