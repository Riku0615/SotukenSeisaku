using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBreathDamage : MonoBehaviour
{
    [SerializeField] float damagePerSecond = 8f;
    bool isBreathing = false;

    void OnTriggerStay(Collider other)
    {
        if (!isBreathing) return;

        if (other.CompareTag("Player"))
        {
            PlayerHP hp = other.GetComponent<PlayerHP>();
            if(hp != null)
            {
                hp.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }

    public void BreathStart()
    {
        isBreathing = true;
    }

    public void BreathEnd()
    {
        isBreathing = false;
    }
}
