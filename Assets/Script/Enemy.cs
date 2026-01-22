using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;    //追跡するプレイヤー
    public float detectionRange = 10.0f;//プレイヤーを見つける範囲
    public float attackRange = 2.0f;   //攻撃する距離
    public float attackCooldown = 2.0f;//攻撃間隔

    private NavMeshAgent agent;
    private Animator animator;
    public BoxCollider CapCollider;
    private float lastAttackTime;

    //[SerializeField]
    //AudioSource walkAudioSource;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        CapCollider.enabled = false;
        //AudioSourceを自動取得
        //if (walkAudioSource == null)
            //walkAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        //bool isMoving = false;

        if (distance <= detectionRange)
        {
            if(agent.enabled)
            {
                agent.SetDestination(player.position);
                //isMoving = true;
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
            if (agent.enabled)
            {
                agent.SetDestination(transform.position);
            }
            animator.SetBool("Move", false);//待機アニメーション
        }
    }

    void Attack()
    {
        agent.enabled = false;//追跡を一時停止
        animator.SetTrigger("Attack");//攻撃アニメーション
        Invoke(nameof(ResumeChase), 1.0f);//1秒後に追跡再開
    }

    void ResumeChase()
    {
        //追跡を再開する
        agent.enabled = true;
    }

    void AttackStart()
    {
        //当たり判定を有効にする
        CapCollider.enabled = true;
        //デバッグ
        Debug.Log("攻撃開始");
    }

    void AttackEnd()
    {
        //当たり判定を無効にする
        CapCollider.enabled = false;
        //デバッグ
        Debug.Log("攻撃終了");
    }
}
