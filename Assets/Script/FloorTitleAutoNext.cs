using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorTitleAutoNext : MonoBehaviour
{
    [SerializeField]
    float waitTime = 2.0f;  //•\¦ŠÔ(•b)

    [SerializeField]
    GameObject FadeCanvas;

    [SerializeField]
    string nextSceneName;   //Ÿ‚ÌŠK‘wƒV[ƒ“–¼

    void Start()
    {
        StartCoroutine(GoNext());
    }

    IEnumerator GoNext()
    {
        yield return new WaitForSeconds(waitTime);
        GameObject fade = Instantiate(FadeCanvas);
        fade.GetComponent<Fade>().FadeStart(nextSceneName);
    }
}
