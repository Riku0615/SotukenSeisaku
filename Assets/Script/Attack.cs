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
        //�G�ꂽ�I�u�W�F�N�g�̃^�O��Enemy�Ȃ�
        if(other.CompareTag("Enemy"))
        {
            //�Ώۂ��폜����
            Destroy(other.gameObject);
        }
        //�G�ꂽ�I�u�W�F�N�g�̃^�O��Boss�Ȃ�
        if(other.CompareTag("Boss"))
        {
            string sceneName = SceneName;
            //���O���󔒂������ꍇ,���݂̃V�[���̖��O���g��
            if(sceneName =="")
            {
                sceneName = SceneManager.GetActiveScene().name;
            }

            //�t�F�[�h�p�L�����o�X�𐶐����ăt�F�[�h�J�n
            GameObject fadeCanvas = Instantiate(FadeCanvas);
            fadeCanvas.GetComponent<Fade>().FadeStart(sceneName);

            //�{�X���폜
            Destroy(other.gameObject);
        }
    }
}
