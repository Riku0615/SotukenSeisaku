using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤー用スクリプトクラス
public class Player : MonoBehaviour
{
    [SerializeField]
    float MoveSpeed = 200.0f;

    [SerializeField]
    AudioClip AttackSE, GuardSE;

    [SerializeField]
    AudioSource walkAudioSource;

    Rigidbody m_rigidBody;
    Animator m_playerAnimator;
    GameObject m_mainCamera;

    //剣の当たり判定
    public BoxCollider SwordCollider;

    public Attack swordAttack;

    bool m_moveFlag;
    public bool isGuard = false;

    //攻撃中かどうか
    bool isAttacking = false;

    //初期配置用変数
    Vector3 initPos = new Vector3(0.0f, 0.0f, 85.0f);

    // Start is called before the first frame update
    void Start()
    {
        //クォータニオンを使って初期回転を設定
        transform.rotation = Quaternion.Euler(0, 180, 0);
        m_rigidBody = GetComponent<Rigidbody>();
        //自分にアタッチされているAnimatorを取得する
        m_playerAnimator = GetComponent<Animator>();
        //剣の当たり判定を無効にする
        SwordCollider.enabled = false;
        //歩きSE用AudioSourceを取得
        if (walkAudioSource == null)
            walkAudioSource = GetComponent<AudioSource>();
        //メインカメラのゲームオブジェクトを取得する
        m_mainCamera = Camera.main.gameObject;
    }

    //初期設定用メソッド
    public void doInit()
    {
        transform.rotation = Quaternion.identity;
        transform.position = initPos;
    }

    // Update is called once per frame
    void Update()
    {
        //防御
        if(Input.GetMouseButton(1)||Input.GetKey(KeyCode.JoystickButton5))//右クリックでガード
        {
            isGuard = true;
            //効果音
            GameManager.PlaySE(GuardSE);
        }
        else
        {
            isGuard = false;
        }
        m_playerAnimator.SetBool("IsGuard", isGuard);
        //攻撃(防御中は攻撃しない)
        if(!isGuard && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton2)))
        {
            PlayAttack1();
        }
        //攻撃2
        if(!isGuard && (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton3)))
        {
            PlayAttack2();
        }
        //アニメーション
        Animation();
    }

    void FixedUpdate()
    {
        //カメラを考慮した移動
        Vector3 PlayerMove = Vector3.zero;
        Vector3 stickL = Vector3.zero;
        stickL.z = Input.GetAxis("Vertical");
        stickL.x = Input.GetAxis("Horizontal");

        Vector3 forward = m_mainCamera.transform.forward;
        Vector3 right = m_mainCamera.transform.right;
        forward.y = 0.0f;
        right.y = 0.0f;
        //移動速度に上記で計算したベクトルを加算する
        PlayerMove = (right * stickL.x + forward * stickL.z).normalized * MoveSpeed * Time.deltaTime;

        float runSpeed = 300.0f;
        //シフトキーが押されたらダッシュする
        if(Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.JoystickButton1))
        {
            MoveSpeed = runSpeed;
            m_playerAnimator.SetTrigger("Run");
        }
        else
        {
            MoveSpeed = 200.0f;
        }

        //移動させる
        PlayerMove = (PlayerMove * MoveSpeed * Time.deltaTime);
        m_rigidBody.velocity = PlayerMove;

        //移動フラグの更新
        bool isMoving = PlayerMove.sqrMagnitude > 0.0f;
        m_moveFlag = isMoving;

        //歩きSEのループ制御
        if(isMoving && !isGuard)
        {
            if(!walkAudioSource.isPlaying)
            {
                walkAudioSource.Play(); //ループ開始
            }
        }
        else
        {
            if(walkAudioSource.isPlaying)
            {
                walkAudioSource.Stop(); //停止
            }
        }

        //回転
        if (isMoving)
        {
            transform.rotation = Quaternion.LookRotation(PlayerMove.normalized);
        }
    }

    void PlayAttack1()
    {
        if (isAttacking) return;

        isAttacking = true;

        //攻撃タイプを1に切り替える
        swordAttack.attackType = Attack.AttackType.Attack1;

        m_playerAnimator.SetTrigger("Attack");
    }

    void PlayAttack2()
    {
        if (isAttacking) return;

        isAttacking = true;

        //攻撃タイプを2に切り替える
        swordAttack.attackType = Attack.AttackType.Attack2;

        m_playerAnimator.SetTrigger("Attack2");
    }

    private void Animation()
    {
        //移動フラグ
        m_playerAnimator.SetBool("MoveFlag", m_moveFlag);
    }

    //攻撃開始
    void AttackStart()
    {
       //当たり判定を有効にする
       SwordCollider.enabled = true;
       GameManager.PlaySE(AttackSE);
       //デバッグ
       Debug.Log("攻撃開始");
    }

    //攻撃終了
    void AttackEnd()
    {
        //当たり判定を無効にする
        SwordCollider.enabled = false;
        //デバッグ
        Debug.Log("攻撃終了");
    }

    //攻撃モーションの最後で呼ぶ
    void AttackFinish()
    {
        isAttacking = false;
    }
}
