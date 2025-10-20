using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーン遷移に必要

public class SceneChangeStairs : MonoBehaviour
{
    [SerializeField]
    GameObject FadeCanvas;
    [SerializeField]
    string SceneName;

    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーかどうかを確認
        if(other.CompareTag("Player"))
        {
            string sceneName = SceneName;

            //名前が空白だった場合、現在のシーンの名前を使う
            if(sceneName =="")
            {
                sceneName = SceneManager.GetActiveScene().name;
            }
            //フェード用のCanvasを作成
            GameObject fadeCanvas = Instantiate(FadeCanvas);
            //FadeSceneを取得してフェードを開始
            fadeCanvas.GetComponent<Fade>().FadeStart(sceneName);
        }
    }
}
