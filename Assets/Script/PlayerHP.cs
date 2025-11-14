using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Image hpBar;
    public float maxHP = 100f;
    public float currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        //テスト用:スペースキーでダメージを受ける
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

        hpBar.fillAmount = currentHP / maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;
    }
}
