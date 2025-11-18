using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Image hpBar;
    public Slider hpSlider;
    public float maxHP = 100f;
    public float currentHP;

    [Header("無敵時間設定")]
    public float invincibleDuration = 2f;//ダメージ後の無敵時間
    private bool isInvincible = false;

    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;

        animator = GetComponent<Animator>();

        //スライダーの初期設定
        if (hpSlider != null)
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
        if (isDead) return; //死亡済みなら変更不可
        if (isInvincible) return; //無敵中はスライダーでも変えさせない!

        currentHP = value;
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        if(isInvincible)
        {
            Debug.Log("無敵中のためダメージ無効!");
            return;
        }

        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        //ダメージアニメーション再生
        animator.SetTrigger("Hit");

        UpdateUI();

        //HPが0で死亡
        if(currentHP<=0)
        {
            Die();
        }
        else
        {
            //ダメージ後無敵時間スタート
            StartCoroutine(InvincibleTime());
        }
    }

    //無敵状態コルーチン
    private IEnumerator InvincibleTime()
    {
        isInvincible = true;
        Debug.Log("無敵時間開始");

        // ここで点滅などの無敵演出も可能
        yield return new WaitForSeconds(invincibleDuration);

        isInvincible = false;
        Debug.Log("無敵解除");
    }

    //UI更新
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
        isDead = true;
        //死亡アニメーション再生
        animator.SetTrigger("Die");
        //プレイヤー操作停止
        var controller = GetComponent<Player>();
        if (controller != null)
            controller.enabled = false;
        //CharacterControllerを切る場合
        var cc = GetComponent<CharacterController>();
        if (cc != null)
            cc.enabled = false;
    }
}