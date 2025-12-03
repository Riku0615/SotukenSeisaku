using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public enum AttackType { Attack1, Attack2 }
    public AttackType attackType;   //攻撃タイプ

    private void OnTriggerEnter(Collider other)
    {
        float damage = 0;
        
        //攻撃タイプでダメージを変える
        if(attackType == AttackType.Attack1)
        {
            damage = Random.Range(1f, 5f);  //攻撃1
        }
        else if(attackType==AttackType.Attack2)
        {
            damage = Random.Range(10f, 20f); //攻撃2
        }

        //触れたオブジェクトのタグがEnemyなら
        if(other.CompareTag("Enemy"))
        {
            //EnemyHPを持っているか確認
            EnemyHP enemyHP = other.GetComponent<EnemyHP>();
            if (enemyHP != null)enemyHP.TakeDamage(damage);
        }
        //触れたオブジェクトのタグがBossなら
        if(other.CompareTag("Boss"))
        {
            //BossHPを持っているか確認
            BossHP bossHP = other.GetComponent<BossHP>();
            if (bossHP != null)bossHP.TakeDamage(damage);
        }
    }
}
