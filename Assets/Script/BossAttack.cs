using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Damage")]
    public float damage = 10f;  //与えるダメージ

    [Header("Hit Effect")]
    [SerializeField]
    ParticleSystem hitEffect;   //ヒット時エフェクト

    [Header("Sound")]
    [SerializeField]
    AudioClip hitSE;   //プレイヤー被弾SE

    [SerializeField]
    AudioClip guardSE;  //ガード成功SE

    private bool canDamage = true;  //攻撃可能かどうか(アニメーションで切り替える...)

    //ボスが何かに接触した瞬間呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return; //攻撃していない間は無視

        //触れたオブジェクトのタグがPlayerなら...
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            PlayerHP hp = other.GetComponent<PlayerHP>();

            if (player == null || hp == null) return;
            //ガード中なら
            if(player.isGuard)
            {
                float guardDamage = damage * 1.0f;
                hp.TakeDamage(guardDamage);
                PlayGuardSE();
                return;
            }
            //ガードしていない場合
            hp.TakeDamage(damage);
            PlayHitEffect(other);
            PlayHitSE();
        }
    }

    void PlayHitEffect(Collider target)
    {
        if (hitEffect == null) return;

        Vector3 hitPos = target.ClosestPoint(transform.position);
        hitEffect.transform.position = hitPos;

        hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        hitEffect.Play();
    }

    void PlayHitSE()
    {
        if(hitSE != null)
        {
            GameManager.PlaySE(hitSE);
        }
    }

    void PlayGuardSE()
    {
        if(guardSE != null)
        {
            GameManager.PlaySE(guardSE);
        }
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
