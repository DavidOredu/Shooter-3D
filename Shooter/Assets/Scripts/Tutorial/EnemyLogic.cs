using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack
}
public class EnemyLogic : MonoBehaviour
{
    int health = 100;

    [SerializeField]
    EnemyState currentState = EnemyState.Idle;

    [SerializeField]
    Transform destination;

    [SerializeField]
    Transform patrolStartPosition;
    [SerializeField]
    Transform patrolEndPosition;

    Vector3 currentPatrolDestination;

    NavMeshAgent navMeshAgent;

    GameObject player;

    [SerializeField]
    int damageAmount = 10;
    [SerializeField]
    float agroRadius = 5f;
    [SerializeField]
    float meleeRadius = 2f;
    [SerializeField]
    float stoppingDistance = 1.5f;

    [SerializeField]
    float maxAttackCooldown = .5f;
    float attackCooldown;

    AudioSource audioSource;

    [SerializeField]
    AudioClip enemyAttackSound;
    [SerializeField]
    AudioClip enemyHitSound;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        currentPatrolDestination = patrolStartPosition.position;
        attackCooldown = maxAttackCooldown;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                SerachForPlayer();
                break;
            case EnemyState.Patrol:
                SerachForPlayer();
                Patrol();
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
            case EnemyState.Attack:
                UpdateAttack();
                break;
        }
    }
    void ChasePlayer()
    {
        navMeshAgent.SetDestination(destination.position);

        float distance = Vector3.Distance(transform.position, destination.position);
        if(distance < stoppingDistance)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;

            currentState = EnemyState.Attack;
        }
        else
        {
            navMeshAgent.isStopped = false;
        }
    }
    void SerachForPlayer()
    {
        float distance = Vector3.Distance(transform.position, destination.position);
        if(distance < agroRadius)
        {
            currentState = EnemyState.Chase;
        }
    }
    void Patrol()
    {
        if(currentPatrolDestination != Vector3.zero)
        {
            navMeshAgent.SetDestination(currentPatrolDestination);
        }

        float distance = Vector3.Distance(transform.position, currentPatrolDestination);
        if (distance < stoppingDistance)
        {
            if(currentPatrolDestination == patrolStartPosition.position)
            {
                currentPatrolDestination = patrolEndPosition.position;
            }
            else
            {
                currentPatrolDestination = patrolStartPosition.position;
            }
        }
    }
    void UpdateAttack()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < meleeRadius)
        {
            attackCooldown -= Time.deltaTime;

            if(attackCooldown < 0)
            {
                // attack player
                PlayerLogic playerLogic = player.GetComponent<PlayerLogic>();
                playerLogic.TakeDamage(damageAmount);

                attackCooldown = maxAttackCooldown;
                PlaySound(enemyAttackSound);
            }
        }
        else
        {
            currentState = EnemyState.Chase;
        }
    }
    void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        PlaySound(enemyHitSound);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .25f);
        Gizmos.DrawSphere(transform.position, agroRadius);
        Gizmos.color = new Color(0, 1, 0, .25f);
        Gizmos.DrawSphere(transform.position, meleeRadius);
        Gizmos.color = new Color(0, 0, 1, .25f);
        Gizmos.DrawSphere(transform.position, stoppingDistance);
    }
}
