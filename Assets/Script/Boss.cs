using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public Transform player;            //追跡するプレイヤー
    public float detectionRange = 100.0f;//プレイヤーを見つける範囲
    //public float attackRange = 2.0f;    //攻撃する距離
    public float attackCooldown = 2.0f; //攻撃間隔

    private NavMeshAgent agent;
    private Animator animator;
    public BoxCollider ToothCollider;
    public CapsuleCollider BreathCollider;
    private float lastAttackTime;

    [Header("Attack Range")]
    public float attackRange = 3.0f;
    public float breathRange = 15.0f;

    [Header("Breath")]
    public GameObject breathPrefab;
    public Transform breathPoint;

    bool isAttacking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ToothCollider.enabled = false;
        BreathCollider.enabled = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
            Idle();
            return;
        }

        if (isAttacking) return;

        if (!agent.enabled)
            agent.enabled = true;

        agent.isStopped = false;
        agent.updateRotation = true;
        agent.SetDestination(player.position);

        animator.SetBool("Move", true);

        if (Time.time - lastAttackTime < attackCooldown) return;

        if (distance <= attackRange)
        {
            Attack();
        }
        else if (distance <= breathRange)
        {
            BreathAttack();
        }
    }

    private void Idle()
    {
        animator.SetBool("Move", false);

        if(agent.enabled)
        {
            agent.isStopped = true;
        }
    }

    void Attack()
    {
        if (isAttacking) return;

        isAttacking = true;

        agent.isStopped = true;        //追跡を一時停止
        agent.updateRotation = false;

        animator.SetTrigger("Attack");  //攻撃アニメーション
        lastAttackTime = Time.time;
        //Invoke(nameof(ResumeChase), 1f);//1秒後に追跡再開
    }

    void EndAttack()
    {
        isAttacking = false;

        agent.isStopped = false;
        agent.updateRotation = true;
    }

    void BreathAttack()
    {
        if (isAttacking) return;

        isAttacking = true;

        agent.isStopped = true;
        agent.updateRotation = false;

        animator.SetTrigger("BreathAttack");
        lastAttackTime = Time.time;
    }

    void ResumeChase()
    {
        //追跡を再開する
        agent.enabled = true;
    }

    void AttackStart()
    {
        //当たり判定を有効にする
        ToothCollider.enabled = true;
        //デバッグ
        Debug.Log("攻撃開始");
    }

    void AttackEnd()
    {
        //当たり判定を無効にする
        ToothCollider.enabled = false;
        //デバッグ
        Debug.Log("攻撃終了");
    }

    GameObject currentBreath;
    
    void BreathStart()
    {
        //当たり判定を有効にする
        BreathCollider.enabled = true;
        //デバッグ
        Debug.Log("BreathStart");

        currentBreath = Instantiate(
            breathPrefab,
            breathPoint.position,
            breathPoint.rotation
        );

        currentBreath.transform.SetParent(breathPoint);
        currentBreath.transform.localScale = Vector3.one;

        var ps = currentBreath.GetComponent<ParticleSystem>();
        if (ps != null) ps.Play();
    }

    void BreathEnd()
    {
        //当たり判定を無効にする
        BreathCollider.enabled = false;

        Destroy(currentBreath);

        isAttacking = false;
        lastAttackTime = Time.time;

        agent.isStopped = false;
        agent.updateRotation = true;
    }
}
