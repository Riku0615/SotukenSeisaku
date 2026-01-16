using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public Image hpBar;
    public Slider hpSlider;
    public float maxHP = 5f;
    public float currentHP;

    private Animator animator;
    private Rigidbody rb;
    private Collider col;
    private NavMeshAgent agent;

    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();

        //スライダーの初期設定
        if(hpSlider != null)
        {
            hpSlider.minValue = 0;
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
            hpSlider.onValueChanged.AddListener(UpdateHPFromSlider);
        }
        UpdateUI();
    }

    public void UpdateHPFromSlider(float value)
    {
        if (isDead) return;//死亡済みなら変更不可
        currentHP = value;
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        UpdateUI();

        //HPが0で死亡
        if(currentHP <= 0)
        {
            Die();
            return;
        }

        //ダメージアニメーション再生(生きてる時だけ)
        animator.SetTrigger("Hit");
    }

    //UI更新処理
    private void UpdateUI()
    {
        if (hpSlider != null)
            hpSlider.value = currentHP;

        if (hpBar != null)
            hpBar.fillAmount = currentHP / maxHP;
    }

    //死亡処理
    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("敵が死亡 (NavMeshAgent 停止)");

        // ===== NavMeshAgent停止 =====
        if (agent != null && agent.isOnNavMesh && agent.enabled)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.enabled = false;
        }

        //Rigidbody停止
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        //当たり判定オフ
        if (col != null)
        {
            col.enabled = false;
        }

        //死亡アニメーション再生
        if (animator != null)
        { 
            animator.SetTrigger("Die");
        }

        //他スクリプト停止(AI・攻撃など)
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour script in scripts)
        {
            if (script != this)
                script.enabled = false;
        }

        StartCoroutine(DieAfterAnimation());
    }

    private IEnumerator DieAfterAnimation()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
