using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラをコントロールするクラス
public class CameraController : MonoBehaviour
{
    //カメラの回転速度
    public float RotSpeed = 1.0f;
    //カメラの回転上限
    public float RotUpLimit = 40.0f;
    //カメラの回転下限
    public float RotDownLimit = -20.0f;
    //プレイヤーとカメラの距離
    public float CameraRange = 15.5f;
    //カメラの高さ
    public float CameraY_Up = 6.15f;

    private GameObject m_player;
    private float m_nowX_Rot = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Playerタグがついたオブジェクトを探す
        m_player = GameObject.FindGameObjectWithTag("Player");
        //初期X軸の回転量を保存
        m_nowX_Rot = transform.localEulerAngles.x;
    }

    void LateUpdate()
    {
        if (m_player == null) return;

        // 右スティック入力（軸名はInputManagerで設定）
        float stickX = Input.GetAxis("RightStickHorizontal"); // 左右
        float stickY = Input.GetAxis("RightStickVertical");   // 上下

        //キーボード入力
        float keyX = 0f;
        float keyY = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) keyX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) keyX = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) keyY = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) keyY = -1f;

        //入力合流(スティック + キーボード)
        float inputX = stickX + keyX;//左右
        float inputY = stickY + keyY;//上下

        // 上下回転
        float Up_rot = -inputY * RotSpeed;
        m_nowX_Rot += Up_rot;

        m_nowX_Rot = Mathf.Clamp(m_nowX_Rot, RotDownLimit, RotUpLimit);

        transform.rotation = Quaternion.Euler(m_nowX_Rot, transform.eulerAngles.y, 0f);

        // 左右回転
        float Left_rot = inputX * RotSpeed;
        transform.RotateAround(m_player.transform.position, Vector3.up, Left_rot);

        // カメラをプレイヤー後方に固定
        Vector3 cameraOffset = -transform.forward * CameraRange + Vector3.up * CameraY_Up;
        transform.position = m_player.transform.position + cameraOffset;
    }
}