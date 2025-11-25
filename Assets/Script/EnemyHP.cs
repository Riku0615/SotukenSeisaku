using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public Image hpBar;
    public Slider hpSlider;
    public float maxHP = 5f;
    public float currentHP;

    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();

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
        Debug.Log("Die()が呼ばれた！");
        if (isDead) return;
        isDead = true;
        //死亡アニメーション再生
        animator.SetTrigger("Die");
        StartCoroutine(DieAfterAnimation());
    }

    private IEnumerator DieAfterAnimation()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
