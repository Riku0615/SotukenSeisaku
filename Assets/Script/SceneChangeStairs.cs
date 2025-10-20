using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //�V�[���J�ڂɕK�v

public class SceneChangeStairs : MonoBehaviour
{
    [SerializeField]
    GameObject FadeCanvas;
    [SerializeField]
    string SceneName;

    private void OnTriggerEnter(Collider other)
    {
        //�v���C���[���ǂ������m�F
        if(other.CompareTag("Player"))
        {
            string sceneName = SceneName;

            //���O���󔒂������ꍇ�A���݂̃V�[���̖��O���g��
            if(sceneName =="")
            {
                sceneName = SceneManager.GetActiveScene().name;
            }
            //�t�F�[�h�p��Canvas���쐬
            GameObject fadeCanvas = Instantiate(FadeCanvas);
            //FadeScene���擾���ăt�F�[�h���J�n
            fadeCanvas.GetComponent<Fade>().FadeStart(sceneName);
        }
    }
}
