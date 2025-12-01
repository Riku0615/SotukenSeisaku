using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public Transform player;            //追跡するプレイヤー
    public float detectionRange = 10.0f;//プレイヤーを見つける範囲
    public float attackRange = 2.0f;    //攻撃する距離
    public float moveSpeed = 5.0f;      //移動速度
    public float rotateSpeed = 5.0f;    //向きの回転速度
    public float attackCooldown = 2.0f; //攻撃間隔

    private Animator animator;
    public BoxCollider BiteCollider;
    private float lastAttackTime;
    private bool canMove = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        BiteCollider.enabled = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            if (distance > attackRange)
            {
                if (canMove)
                {
                    MoveToPlayer(); //プレイヤーへ向かって飛ぶ
                }
            }
            else
            {
                TryAttack();    //攻撃を試みる
            }
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    void MoveToPlayer()
    {
        animator.SetBool("Move", true);

        //プレイヤー方向
        Vector3 dir = (player.position - transform.position).normalized;

        //向きを滑らかに回す
        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);

        //前方向へ移動
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void TryAttack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            StartCoroutine(AttackSequence());
            lastAttackTime = Time.time;
        }
    }

    IEnumerator AttackSequence()
    {
        canMove = false;                    //移動停止
        animator.SetTrigger("Attack");      //攻撃アニメーション再生
        yield return new WaitForSeconds(1f);//攻撃時間分待機
        canMove = true;                     //移動再開
    }

    void AttackStart()
    {
        //当たり判定を有効にする
        BiteCollider.enabled = true;
        //デバッグ
        Debug.Log("攻撃開始");
    }

    void AttackEnd()
    {
        //当たり判定を無効にする
        BiteCollider.enabled = false;
        //デバッグ
        Debug.Log("攻撃終了");
    }
}
