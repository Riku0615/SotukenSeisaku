using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    bool m_fadeStart = false; //true�Ȃ��A�̏������J�n
    bool m_fadeMode = false; //false�Ȃ�Â��Ȃ�true�Ȃ疾�邭�Ȃ�
    float m_alpha = 0.0f;   //�摜�̕s�����x

    [SerializeField]
    float FadeSpeed = 1.0f; //�t�F�[�h�̑��x�B

    //�J�ڐ�̃V�[������ۑ�
    string m_sceneName;
    //���g���g�p����Image��ۑ�
    Image m_image;
    //�t�F�[�h�J�n
    public void FadeStart(string sceneName)
    {
        //�t�F�[�h�J�n�̏���������
        m_fadeStart = true;
        m_sceneName = sceneName;
        //�����̎q�I�u�W�F�N�g�ɃA�^�b�`����Ă���Image���擾����
        m_image = transform.GetChild(0).GetComponent<Image>();
        //���g�̓V�[�����܂����ł��폜����Ȃ��悤�ɂ���
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        //�t�F�[�h���J�n���Ă��Ȃ��Ȃ璆�f
        if(m_fadeStart == false)
        {
            return;
        }

        //�t�F�[�h����
        if(m_fadeMode == false)
        {
            //��ʂ��Â�����
            m_alpha += FadeSpeed * Time.deltaTime;

            //���S�ɈÂ��Ȃ����̂ŃV�[����ύX����
            if(m_alpha >= 1.0f)
            {
                SceneManager.LoadScene(m_sceneName);
                //���邭���郂�[�h�ɕύX
                m_fadeMode = true;
            }
        }
        else
        {
            //��ʂ𖾂邭����
            m_alpha -= FadeSpeed * Time.deltaTime;

            //���S�ɖ��邭�Ȃ����̂Ŏ��g���폜����
            if(m_alpha <= 0.0f)
            {
                Destroy(gameObject);
            }
        }

        //�Ō�ɕs�����x��ݒ肷��
        Color nowColor = m_image.color;
        nowColor.a = m_alpha;
        m_image.color = nowColor;
    }
}
