using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //�V�[���J�ڂɕK�v

public class SceneChange : MonoBehaviour
{
    [SerializeField]
    GameObject FadeCanvas;
    [SerializeField]
    string SceneName;

    // Update is called once per frame
    void Update()
    {
        //�X�y�[�X�L�[�������ꂽ��V�[����؂�ւ���
        if(Input.GetKeyDown(KeyCode.Space))
        {
            string sceneName = SceneName;

            //���O���󔒂������ꍇ,���݂̃V�[���̖��O���g��
            if(sceneName == "")
            {
                sceneName = SceneManager.GetActiveScene().name;
            }
            //�t�F�[�h�p��Canvas�𐶐�
            GameObject fadeCanvas = Instantiate(FadeCanvas);
            //FadeScene���擾���ăt�F�[�h���J�n
            fadeCanvas.GetComponent<Fade>().FadeStart(sceneName);
        }
    }
}
