using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //効果音再生関数 どこからでも呼べる
    static public OneShotAudioClip PlaySE(AudioClip clip,
        GameObject sauceObject = null,
        float volume=1.0f,
        float spatialBlend=0.0f,
        float minDistance=0.0f,
        float maxDistance=0.0f)
    {
        //効果音オブジェクトを生成
        GameObject oneShotObj = Instantiate((GameObject)Resources.Load("OneShotSE"));
        //座標を設定
        if(sauceObject !=null)
        {
            oneShotObj.transform.position = sauceObject.transform.position;
        }

        //オーディオクリップを設定
        OneShotAudioClip oneShotAudio = oneShotObj.GetComponent<OneShotAudioClip>();
        oneShotAudio.PlaySE(clip, volume,
            spatialBlend, minDistance, maxDistance);

        return oneShotAudio;
    }
}
