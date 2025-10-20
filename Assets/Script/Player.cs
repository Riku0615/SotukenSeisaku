using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤー用スクリプトクラス
public class Player : MonoBehaviour
{
    [SerializeField]
    float MoveSpeed = 150.0f;

    Rigidbody m_rigidBody;
    Animator m_playerAnimator;
    GameObject m_mainCamera;

    //剣の当たり判定
    public BoxCollider SwordCollider;

    bool m_moveFlag;

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
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //移動速度に上記で計算したベクトルを加算する
        PlayerMove = (right * stickL.x + forward * stickL.z).normalized * MoveSpeed * Time.deltaTime;

        float runSpeed = 300.0f;
        //シフトキーが押されたらダッシュする
        if(Input.GetKey(KeyCode.RightShift))
        {
            MoveSpeed = runSpeed;
            m_playerAnimator.SetTrigger("Run");
        }
        else
        {
            MoveSpeed = 150.0f;
        }

        //移動させる
        PlayerMove = (PlayerMove * MoveSpeed * Time.deltaTime);
        m_rigidBody.velocity = PlayerMove;

        //移動フラグの更新
        bool isMoving = PlayerMove.sqrMagnitude > 0.0f;
        m_moveFlag = isMoving;

        //回転
        if (isMoving)
        {
            transform.rotation = Quaternion.LookRotation(PlayerMove.normalized);
        }

        //左クリックが押されたら攻撃アニメーションを再生
        if(Input.GetMouseButtonDown(0))
        {
            m_playerAnimator.SetTrigger("Attack");
        }
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
}
