using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Player player;
    private Animator animator;
    private bool isDead = false;

    [Header("シーン遷移設定")]
    public GameObject fadeCanvasPrefab; //フェード用Canvas
    public string nextSceneName;        //遷移先シーン名

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

        if(isInvincible) return;
        //防御中ならダメージ軽減し,ダメージアニメーションを再生しない
        if(player.isGuard)
        {
            damage *= 0.5f;
        }
        else
        {
            //防御してない時だけダメージアニメーション再生
            animator.SetTrigger("Hit");
        }
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;
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
        // ここで点滅などの無敵演出も可能
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
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
        if (isDead) return;
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
        //死亡アニメーション再生終了を待ってから遷移
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        //死亡アニメーションの長さを取得
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        float animTime = state.length;
        //その時間だけ待つ
        yield return new WaitForSeconds(animTime);
        //フェード開始
        GameObject fadeCanvas = Instantiate(fadeCanvasPrefab);
        fadeCanvas.GetComponent<Fade>().FadeStart(
            nextSceneName == "" ? SceneManager.GetActiveScene().name : nextSceneName
        );
    }
}