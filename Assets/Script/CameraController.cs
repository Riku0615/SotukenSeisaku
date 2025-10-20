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
    public float CameraRange = 12.5f;
    //カメラの高さ
    public float CameraY_Up = 4.95f;

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

    // Update is called once per frame
    void LateUpdate()
    {
        if (m_player == null) return;
        //上下
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

        //上下角度制限
        m_nowX_Rot += Up_rot;
        if(m_nowX_Rot>RotUpLimit||m_nowX_Rot<RotDownLimit)
        {
            m_nowX_Rot = Mathf.Clamp(m_nowX_Rot, RotDownLimit, RotUpLimit);
            Up_rot = 0.0f;
        }
        transform.RotateAround(m_player.transform.position, this.transform.right, Up_rot);

        //左右
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

        //カメラ位置をプレイヤーの後ろに固定
        Vector3 cameraOffset = -transform.forward * CameraRange + Vector3.up * CameraY_Up;
        transform.position = m_player.transform.position + cameraOffset;
    }
}