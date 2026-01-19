using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public enum AttackType { Attack1, Attack2 }
    public AttackType attackType;   //攻撃タイプ

    [Header("Hit Effects")]
    [SerializeField]
    ParticleSystem attack1HitEffect;   //通常攻撃

    [SerializeField]
    ParticleSystem attack2HitEffect;    //攻撃2専用

    [Header("Hit Sound")]
    [SerializeField]
    AudioClip hitSE;    //ヒット時SE

    private void OnTriggerEnter(Collider other)
    {
        float damage = 0;
        
       switch (attackType)
        {
            case AttackType.Attack1:
                damage = Random.Range(1f, 5f);
                break;

            case AttackType.Attack2:
                damage = Random.Range(1f, 5f);
                break;
        }

        //触れたオブジェクトのタグがEnemyなら
        if(other.CompareTag("Enemy"))
        {
            //EnemyHPを持っているか確認
            EnemyHP enemyHP = other.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(damage);
                PlayHitEffect(other);
                PlayHitSE();
            }
        }
        //触れたオブジェクトのタグがBossなら
        if(other.CompareTag("Boss"))
        {
            //BossHPを持っているか確認
            BossHP bossHP = other.GetComponent<BossHP>();
            if (bossHP != null)
            {
                bossHP.TakeDamage(damage);
                PlayHitEffect(other);
                PlayHitSE();
            }
        }
    }

    void PlayHitEffect(Collider target)
    {
        //ヒット位置にエフェクト表示
        Vector3 hitPos = target.ClosestPoint(transform.position);

        if(attackType == AttackType.Attack2 && attack2HitEffect != null)
        {
            attack2HitEffect.transform.position = hitPos;
            attack2HitEffect.Play();
        }
        else if(attack1HitEffect !=null)
        {
            attack1HitEffect.transform.position = hitPos;
            attack1HitEffect.Play();
        }
    }

    void PlayHitSE()
    {
        if(hitSE != null)
        {
            GameManager.PlaySE(hitSE);
        }
    }
}
