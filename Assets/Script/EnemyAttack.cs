using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Damage")]
    public float damage = 10f;      //与えるダメージ

    [Header("Hit Effect")]
    [SerializeField]
    ParticleSystem hitEffect;   //ヒット時エフェクト

    private bool canDamage = true; //攻撃可能かどうか(アニメーションで切り替える)...

    //エネミーに何かが接触した瞬間呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return; //攻撃していない間は無視

        //触れたオブジェクトのタグがPlayerなら...
        if(other.CompareTag("Player"))
        {
            PlayerHP hp = other.GetComponent<PlayerHP>();
            if(hp != null)
            {
                hp.TakeDamage(damage);
                PlayHitEffect(other);
            }
        }
    }

    void PlayHitEffect(Collider target)
    {
        if (hitEffect == null) return;

        Vector3 hitPos = target.ClosestPoint(transform.position);
        hitEffect.transform.position = hitPos;
        hitEffect.Play();
    }

    //アニメーションイベントから呼び出す
    public void AttackStart()
    {
        canDamage = true;
    }
    public void AttackEnd()
    {
        canDamage = false;
    }
}
