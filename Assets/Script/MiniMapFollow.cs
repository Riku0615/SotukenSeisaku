using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player; //プレイヤー
    public float height = 29.95f;  //カメラの高さ
    public float zOffset = -27.5f;  //Z方向のオフセット

    void LateUpdate()
    {
        //プレイヤーの位置に追従
        Vector3 newPos = player.position;
        newPos.y += height;
        newPos.z += zOffset;
        transform.position = newPos;

        //プレイヤーのY回転だけ反映
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
