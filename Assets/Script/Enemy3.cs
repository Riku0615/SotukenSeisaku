using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3 : MonoBehaviour
{
    public Transform player;            //�ǐՂ���v���C���[
    public float detectionRange = 10.0f;//�v���C���[��������͈�
    public float attackRange = 2.0f;    //�U�����鋗��
    public float attackCooldown = 2.0f; //�U���Ԋu

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

        if(distance <= detectionRange)
        {
            if(agent.enabled)
            {
                agent.SetDestination(player.position);
            }
            animator.SetBool("Move", true);//�ړ��A�j���[�V����

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
            animator.SetBool("Move", false);//�ҋ@�A�j���[�V����
        }
    }

    void Attack()
    {
        agent.enabled = false;//�ǐՂ��ꎞ��~
        animator.SetTrigger("Attack");//�U���A�j���[�V����
        Invoke(nameof(ResumeChase), 1.0f);//1�b��ɒǐՍĊJ
    }

    void ResumeChase()
    {
        //�ǐՂ��ĊJ����
        agent.enabled = true;
    }
}
