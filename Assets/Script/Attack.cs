using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
{
    [SerializeField]
    GameObject FadeCanvas;
    [SerializeField]
    string SceneName;

    private void OnTriggerEnter(Collider other)
    {
        //攻撃力をランダムに決定
        float randomDamage = Random.Range(1f, 5f);
        //触れたオブジェクトのタグがEnemyなら
        if(other.CompareTag("Enemy"))
        {
            //EnemyHPを持っているか確認
            EnemyHP enemyHP = other.GetComponent<EnemyHP>();
            if (enemyHP !=null)
            {
                enemyHP.TakeDamage(randomDamage);
            }
        }
        //触れたオブジェクトのタグがBossなら
        if(other.CompareTag("Boss"))
        {
            float randomDamageBoss = Random.Range(5f, 20f);

            string sceneName = SceneName;
            //名前が空白だった場合,現在のシーンの名前を使う
            if(sceneName =="")
            {
                sceneName = SceneManager.GetActiveScene().name;
            }

            //フェード用キャンバスを生成してフェード開始
            GameObject fadeCanvas = Instantiate(FadeCanvas);
            fadeCanvas.GetComponent<Fade>().FadeStart(sceneName);

            //ボスを削除
            Destroy(other.gameObject);
        }
    }
}
