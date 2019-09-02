using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{

    private EnemyAnimator enemyAnim;
    private NavMeshAgent navAgent;
    private EnemyState enemyState;
    public float walkSpeed = 0.5f;
    public float runSpeed = 4;
    public float chaseDistance = 7;
    private float curChaseDistance;
    public float attackDist = 2.2f;
    public float chaseAfterAttackDist = 4f;
    public float patrolRadiusMin = 20;
    public float patrolRadiusMax = 60;
    public float patrolForThisTime = 15;
    private float patrolTimer;
    public float waitBeforeAttack = 2;
    private float attackTimer;
    private Transform target;
    public GameObject attackPoint;

    void Awake()
    {
        enemyAnim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }

    private void Start()
    {
        enemyState = EnemyState.PATROL;
        patrolTimer = patrolForThisTime;
        attackTimer = waitBeforeAttack;
        curChaseDistance = chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.PATROL:
                Patrol();
                break;
            case EnemyState.CHASE:
                Chase();
                break;
            case EnemyState.ATTACK:
                Attack();
                break;
        }
    }

    void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;
        patrolTimer += Time.deltaTime;
        if(patrolTimer > patrolForThisTime)
        {
            SetNewRandomDest();
            patrolTimer = 0;
        }
        if(navAgent.velocity.sqrMagnitude > 0) // moving
        {
            enemyAnim.Walk(true);
        } else
        {
            enemyAnim.Walk(false);
        }

        if(Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyAnim.Walk(false);
            enemyState = EnemyState.CHASE;
            // TODO: audio

        }
    }



    void Chase()
    {
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;
        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0) // moving
        {
            enemyAnim.Run(true);
        }
        else
        {
            enemyAnim.Run(false);
        }

        if (Vector3.Distance(transform.position, target.position) <= attackDist)
        {
            enemyAnim.Run(false);
            enemyAnim.Walk(false);
            enemyState = EnemyState.ATTACK;
            if(chaseDistance != curChaseDistance)
            {
                chaseDistance = curChaseDistance;
            }
        } else if (Vector3.Distance(transform.position, target.position) > chaseDistance)
        {
            enemyAnim.Run(false);
            enemyState = EnemyState.PATROL;
            patrolTimer = patrolForThisTime;
            if(chaseDistance != curChaseDistance)
            {
                chaseDistance = curChaseDistance;
            }
        }

    }

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        attackTimer += Time.deltaTime;

        if (attackTimer > waitBeforeAttack)
        {
            enemyAnim.Attack();
            attackTimer = 0;

            // TODO: attack sound
        }

        if (Vector3.Distance(transform.position, target.position) > attackDist +  chaseAfterAttackDist)
        {
            enemyState = EnemyState.CHASE;
        }
    }

    void SetNewRandomDest()
    {
        // FIX: specify the Random from UnityEngine, not System
        float randRadius = UnityEngine.Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 randDir = UnityEngine.Random.insideUnitSphere * randRadius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);
        navAgent.SetDestination(navHit.position);
    }

    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }

    public EnemyState EnemyState
    {
        get; set;
    }
}
