using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�J�������R���g���[������N���X
public class CameraController : MonoBehaviour
{
    //�J�����̉�]���x
    public float RotSpeed = 1.0f;
    //�J�����̉�]���
    public float RotUpLimit = 40.0f;
    //�J�����̉�]����
    public float RotDownLimit = -20.0f;
    //�v���C���[�ƃJ�����̋���
    public float CameraRange = 12.5f;
    //�J�����̍���
    public float CameraY_Up = 4.95f;

    private GameObject m_player;
    private float m_nowX_Rot = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Player�^�O�������I�u�W�F�N�g��T��
        m_player = GameObject.FindGameObjectWithTag("Player");
        //����X���̉�]�ʂ�ۑ�
        m_nowX_Rot = transform.localEulerAngles.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (m_player == null) return;
        //�㉺
        float Up_rot = 0.5f;
        if(Input.GetKey(KeyCode.UpArrow))
        {
            Up_rot = RotSpeed;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            Up_rot = -RotSpeed;
        }
        else
        {
            Up_rot = 0.0f;
        }

        //�㉺�p�x����
        m_nowX_Rot += Up_rot;
        if(m_nowX_Rot>RotUpLimit||m_nowX_Rot<RotDownLimit)
        {
            m_nowX_Rot = Mathf.Clamp(m_nowX_Rot, RotDownLimit, RotUpLimit);
            Up_rot = 0.0f;
        }
        transform.RotateAround(m_player.transform.position, this.transform.right, Up_rot);

        //���E
        float Left_rot;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Left_rot = RotSpeed;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            Left_rot = -RotSpeed;
        }
        else
        {
            Left_rot = 0.0f;
        }
        transform.RotateAround(m_player.transform.position, Vector3.up, Left_rot);

        //�J�����ʒu���v���C���[�̌��ɌŒ�
        Vector3 cameraOffset = -transform.forward * CameraRange + Vector3.up * CameraY_Up;
        transform.position = m_player.transform.position + cameraOffset;
    }
}