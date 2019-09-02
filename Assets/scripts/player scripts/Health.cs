using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    private EnemyAnimator enemyAnim;
    private NavMeshAgent navAgent;
    private EnemyController ec;

    public float health = 100;
    public bool isPlayer, isBoar, isCannibal;

    private bool isDead;

    void Awake()
    {
        if(isBoar || isCannibal)
        {
            enemyAnim = GetComponent<EnemyAnimator>();
            ec = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
            // TODO: audio, stats
        }
    }

    public void ApplyDamage(float damage)
    {
        if (isDead) return;
        health -= damage;
        if(isPlayer)
        {
            // show health
        }

        if(isBoar || isCannibal)
        {
            if(ec.EnemyState == EnemyState.PATROL)
            {
                ec.chaseDistance = 50;
            }
        }

        if(health < 0)
        {
            Died();
            isDead = true;
        }
    }

    public void Died()
    {
        if(isCannibal)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);
        }
    }
}
