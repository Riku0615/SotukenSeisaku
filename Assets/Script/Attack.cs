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
        //触れたオブジェクトのタグがEnemyなら
        if(other.CompareTag("Enemy"))
        {
            //対象を削除する
            Destroy(other.gameObject);
        }
        //触れたオブジェクトのタグがBossなら
        if(other.CompareTag("Boss"))
        {
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
