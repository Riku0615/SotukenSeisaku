using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //攻撃力をランダムに決定
        float randomDamage = Random.Range(1f, 5f);
        //触れたオブジェクトのタグがEnemyなら
        if(other.CompareTag("Enemy"))
        {
            //EnemyHPを持っているか確認
            EnemyHP enemyHP = other.GetComponent<EnemyHP>();
            if (enemyHP !=null)
            {
                enemyHP.TakeDamage(randomDamage);
            }
        }
        //触れたオブジェクトのタグがBossなら
        if(other.CompareTag("Boss"))
        {
            float randomDamageBoss = Random.Range(5f, 20f);
            //BossHPを持っているか確認
            BossHP bossHP = other.GetComponent<BossHP>();
            if (bossHP !=null)
            {
                bossHP.TakeDamage(randomDamage);
            }
        }
    }
}
