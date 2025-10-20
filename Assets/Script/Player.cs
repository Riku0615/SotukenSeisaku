using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�p�X�N���v�g�N���X
public class Player : MonoBehaviour
{
    [SerializeField]
    float MoveSpeed = 150.0f;

    Rigidbody m_rigidBody;
    Animator m_playerAnimator;
    GameObject m_mainCamera;

    //���̓����蔻��
    public BoxCollider SwordCollider;

    bool m_moveFlag;

    //�����z�u�p�ϐ�
    Vector3 initPos = new Vector3(0.0f, 0.0f, 85.0f);
    // Start is called before the first frame update
    void Start()
    {
        //�N�H�[�^�j�I�����g���ď�����]��ݒ�
        transform.rotation = Quaternion.Euler(0, 180, 0);
        m_rigidBody = GetComponent<Rigidbody>();
        //�����ɃA�^�b�`����Ă���Animator���擾����
        m_playerAnimator = GetComponent<Animator>();
        //���̓����蔻��𖳌��ɂ���
        SwordCollider.enabled = false;
        //���C���J�����̃Q�[���I�u�W�F�N�g���擾����
        m_mainCamera = Camera.main.gameObject;
    }

    //�����ݒ�p���\�b�h
    public void doInit()
    {
        transform.rotation = Quaternion.identity;
        transform.position = initPos;
    }

    // Update is called once per frame
    void Update()
    {
        //�A�j���[�V����
        Animation();
    }

    void FixedUpdate()
    {
        //�J�������l�������ړ�
        Vector3 PlayerMove = Vector3.zero;
        Vector3 stickL = Vector3.zero;
        stickL.z = Input.GetAxis("Vertical");
        stickL.x = Input.GetAxis("Horizontal");

        Vector3 forward = m_mainCamera.transform.forward;
        Vector3 right = m_mainCamera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //�ړ����x�ɏ�L�Ōv�Z�����x�N�g�������Z����
        PlayerMove = (right * stickL.x + forward * stickL.z).normalized * MoveSpeed * Time.deltaTime;

        float runSpeed = 300.0f;
        //�V�t�g�L�[�������ꂽ��_�b�V������
        if(Input.GetKey(KeyCode.RightShift))
        {
            MoveSpeed = runSpeed;
            m_playerAnimator.SetTrigger("Run");
        }
        else
        {
            MoveSpeed = 150.0f;
        }

        //�ړ�������
        PlayerMove = (PlayerMove * MoveSpeed * Time.deltaTime);
        m_rigidBody.velocity = PlayerMove;

        //�ړ��t���O�̍X�V
        bool isMoving = PlayerMove.sqrMagnitude > 0.0f;
        m_moveFlag = isMoving;

        //��]
        if (isMoving)
        {
            transform.rotation = Quaternion.LookRotation(PlayerMove.normalized);
        }

        //���N���b�N�������ꂽ��U���A�j���[�V�������Đ�
        if(Input.GetMouseButtonDown(0))
        {
            m_playerAnimator.SetTrigger("Attack");
        }
    }

    private void Animation()
    {
        //�ړ��t���O
        m_playerAnimator.SetBool("MoveFlag", m_moveFlag);
    }

    //�U���J�n
    void AttackStart()
    {
        //�����蔻���L���ɂ���
        SwordCollider.enabled = true;
        //�f�o�b�O
        Debug.Log("�U���J�n");
    }

    //�U���I��
    void AttackEnd()
    {
        //�����蔻��𖳌��ɂ���
        SwordCollider.enabled = false;
        //�f�o�b�O
        Debug.Log("�U���I��");
    }
}
