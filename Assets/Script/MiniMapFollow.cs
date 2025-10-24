using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player; //�v���C���[
    public float height = 29.95f;  //�J�����̍���
    public float zOffset = -27.5f;  //Z�����̃I�t�Z�b�g

    void LateUpdate()
    {
        //�v���C���[�̈ʒu�ɒǏ]
        Vector3 newPos = player.position;
        newPos.y += height;
        newPos.z += zOffset;
        transform.position = newPos;

        //�v���C���[��Y��]�������f
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
