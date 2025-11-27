using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーン遷移に必要

public class SceneChangeStairs : MonoBehaviour
{
    [SerializeField]
    GameObject FadeCanvas;
    [SerializeField]
    string SceneName;
    //階段を上るときの効果音
    public AudioClip StairsSE;
    //効果音再生用オブジェクト
    public GameObject OneShotPrefab;

    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーかどうかを確認
        if (other.CompareTag("Player"))
        {
            //階段を上るときの効果音を再生
            GameObject oneShotObj = Instantiate(OneShotPrefab);
            oneShotObj.GetComponent<OneShotAudioClip>().PlaySE(StairsSE);

            string sceneName = SceneName;

            //名前が空白だった場合、現在のシーンの名前を使う
            if (sceneName =="")
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
