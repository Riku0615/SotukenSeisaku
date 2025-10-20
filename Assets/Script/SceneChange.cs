using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーン遷移に必要

public class SceneChange : MonoBehaviour
{
    [SerializeField]
    GameObject FadeCanvas;
    [SerializeField]
    string SceneName;

    // Update is called once per frame
    void Update()
    {
        //スペースキーが押されたらシーンを切り替える
        if(Input.GetKeyDown(KeyCode.Space))
        {
            string sceneName = SceneName;

            //名前が空白だった場合,現在のシーンの名前を使う
            if(sceneName == "")
            {
                sceneName = SceneManager.GetActiveScene().name;
            }
            //フェード用のCanvasを生成
            GameObject fadeCanvas = Instantiate(FadeCanvas);
            //FadeSceneを取得してフェードを開始
            fadeCanvas.GetComponent<Fade>().FadeStart(sceneName);
        }
    }
}
