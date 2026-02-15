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

    [Header("ダメージ硬直")]
    public float hitStopTime = 1.5f;//ダメージアニメーションの硬直時間

    [Header("点滅設定")]
    public float blinkInterval = 0.1f;//点滅間隔
    private Renderer[] renderers;

    void Start()
    {
        currentHP = maxHP;

        animator = GetComponent<Animator>();

        renderers = GetComponentsInChildren<Renderer>();

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
            StartCoroutine(HitStop());
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
            if(player.isGuard)
            {
                //ガード中は無敵だけ付与(点滅なし)
                StartCoroutine(InvincibleOnly());
            }
            else
            {
                //通常ヒットは点滅付き無敵
                StartCoroutine(InvincibleTime());
            }
        }
    }

    //無敵状態コルーチン
    private IEnumerator InvincibleTime()
    {
        isInvincible = true;

        float timer = 0f;

        while(timer < invincibleDuration)
        {
            SetRenderersEnabled(false);
            yield return new WaitForSeconds(blinkInterval);

            SetRenderersEnabled(true);
            yield return new WaitForSeconds(blinkInterval);

            timer += blinkInterval * 2;
        }

        //最後は必ず表示状態に戻す
        SetRenderersEnabled(true);

        isInvincible = false;
    }

    private IEnumerator InvincibleOnly()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibleDuration);

        isInvincible = false;
    }

    //操作停止用コルーチン
    private IEnumerator HitStop()
    {
        //操作停止
        player.canMove = false;

        //物理的にも完全停止したい場合
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        yield return new WaitForSeconds(hitStopTime);

        //死亡していなければ操作再開
        if (!isDead)
            player.canMove = true;
    }
    
    private void SetRenderersEnabled(bool enabled)
    {
        foreach(Renderer r in renderers)
        {
            r.enabled = enabled;
        }
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