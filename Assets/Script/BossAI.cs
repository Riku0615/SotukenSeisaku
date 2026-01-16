using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] Transform player;

    [Header("Attack Distance")]
    [SerializeField] float closeAttackDistance = 3f;
    [SerializeField] float breathAttackDistance = 6f;

    [Header("Cooldown")]
    [SerializeField] float attackCooldown = 2f;

    Animator animator;
    bool canAttack = true; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canAttack) return;

        float distance =
            Vector3.Distance(transform.position, player.position);

        if (distance <= closeAttackDistance)
        {
            StartAttack("CloseAttack");
        }
        else if (distance <= breathAttackDistance)
        {
            StartAttack("BreathAttack");
        }
    }

    void StartAttack(string trigger)
    {
        canAttack = false;
        animator.SetTrigger(trigger);
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}
