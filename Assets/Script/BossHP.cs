using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHP : MonoBehaviour
{
    public Image hpBar;
    public Slider hpSlider;
    public float maxHP = 300f;
    public float currentHP;

    private Animator animator;
    private bool isDead = false;

    public GameObject FadeCanvas;
    public string clearSceneName = "GameClear";

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();

        //スライダーの初期設定
        if(hpSlider !=null)
        {
            hpSlider.minValue = 0;
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        UpdateUI();

        //HPが0で死亡
        if(currentHP<=0)
        {
            Die();
            return;
        }
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
        Debug.Log("Die()が呼ばれた!");
        if (isDead) return;
        isDead = true;
        //死亡アニメーション再生
        animator.SetTrigger("Die");
        //死亡後に消える
        StartCoroutine(DieAfterAnimation());
    }

    private IEnumerator DieAfterAnimation()
    {
        //死亡アニメーションが終わるまで待つ
        yield return new WaitForSeconds(2f);

        // HPバーを非表示に
        if (hpSlider != null)
            hpSlider.gameObject.SetActive(false);

        if (hpBar != null)
            hpBar.gameObject.SetActive(false);

        //フェード開始
        if(FadeCanvas !=null)
        {
            GameObject fade = Instantiate(FadeCanvas);
            fade.GetComponent<Fade>().FadeStart(clearSceneName);
        }

        Destroy(gameObject);
    }
}
