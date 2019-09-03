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
    private EnemyAudio enemyAudio;
    private PlayerStats stats;

    void Awake()
    {
        if(isBoar || isCannibal)
        {
            enemyAnim = GetComponent<EnemyAnimator>();
            ec = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();

            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }
        if(isPlayer)
        {
            stats = GetComponent<PlayerStats>();
        }
    }

    public void ApplyDamage(float damage)
    {
        print("ad p:" + isPlayer + " b:" + isBoar + " c:" + isCannibal);
        if (isDead) return;

        health -= damage;

        if(isPlayer)
        {
            stats.DispalyHealthStats(health);
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
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 20f);

            ec.enabled = false;
            navAgent.enabled = false;
            enemyAnim.enabled = false;


            StartCoroutine(DeadSound());
        } else if(isBoar)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            ec.enabled = false;
            enemyAnim.Dead();

            StartCoroutine(DeadSound());
        } else if(isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().enabled = false;
            }

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentWeapon().gameObject.SetActive(false);
        }

        if(tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3);
        } else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.PlayDead();
    }
}
