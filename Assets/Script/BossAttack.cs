using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float damage = 10f;      //与えるダメージ
    private bool canDamage = true;  //攻撃可能かどうか(アニメーションで切り替える...)

    //ボスが何かに接触した瞬間呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return; //攻撃していない間は無視

        //触れたオブジェクトのタグがPlayerなら...
        if (other.CompareTag("Player"))
        {
            PlayerHP hp = other.GetComponent<PlayerHP>();
            if(hp !=null)
            {
                hp.TakeDamage(damage);
            }
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
