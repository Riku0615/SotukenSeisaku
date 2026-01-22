using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBreathDamage : MonoBehaviour
{
    public float damage = 8f;
    public float damageInterval = 0.5f;

    float lastDamageTime;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (Time.time - lastDamageTime < damageInterval) return;

        lastDamageTime = Time.time;

        //プレイヤーにダメージ
        other.GetComponent<PlayerHP>().TakeDamage(damage);
    }
}
