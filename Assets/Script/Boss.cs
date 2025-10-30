using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public Transform player;            //追跡するプレイヤー
    public float detectionRange = 10.0f;//プレイヤーを見つける範囲
    public float attackRange = 2.0f;    //攻撃する距離
    public float attackCooldown = 2.0f; //攻撃間隔

    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            if(agent.enabled)
            {
                agent.SetDestination(player.position);
            }
            animator.SetBool("Move", true);//移動アニメーション

            if(distance <= attackRange)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            if(agent.enabled)
            {
                agent.SetDestination(transform.position);
            }
            animator.SetBool("Move", false);//待機アニメーション
        }
    }

    void Attack()
    {
        agent.isStopped = false;        //追跡を一時停止
        animator.SetTrigger("Attack");  //攻撃アニメーション
        Invoke(nameof(ResumeChase), 1f);//1秒後に追跡再開
    }

    void ResumeChase()
    {
        agent.isStopped = false;
    }
}
