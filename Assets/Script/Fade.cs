using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    bool m_fadeStart = false; //trueなら一連の処理を開始
    bool m_fadeMode = false; //falseなら暗くなるtrueなら明るくなる
    float m_alpha = 0.0f;   //画像の不透明度

    [SerializeField]
    float FadeSpeed = 1.0f; //フェードの速度。

    //遷移先のシーン名を保存
    string m_sceneName;
    //自身が使用するImageを保存
    Image m_image;
    //フェード開始
    public void FadeStart(string sceneName)
    {
        //フェード開始の準備をする
        m_fadeStart = true;
        m_sceneName = sceneName;
        //自分の子オブジェクトにアタッチされているImageを取得する
        m_image = transform.GetChild(0).GetComponent<Image>();
        //自身はシーンをまたいでも削除されないようにする
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        //フェードが開始していないなら中断
        if(m_fadeStart == false)
        {
            return;
        }

        //フェード処理
        if(m_fadeMode == false)
        {
            //画面を暗くする
            m_alpha += FadeSpeed * Time.deltaTime;

            //完全に暗くなったのでシーンを変更する
            if(m_alpha >= 1.0f)
            {
                SceneManager.LoadScene(m_sceneName);
                //明るくするモードに変更
                m_fadeMode = true;
            }
        }
        else
        {
            //画面を明るくする
            m_alpha -= FadeSpeed * Time.deltaTime;

            //完全に明るくなったので自身を削除する
            if(m_alpha <= 0.0f)
            {
                Destroy(gameObject);
            }
        }

        //最後に不透明度を設定する
        Color nowColor = m_image.color;
        nowColor.a = m_alpha;
        m_image.color = nowColor;
    }
}
