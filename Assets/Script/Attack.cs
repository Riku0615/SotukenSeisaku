using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //�G�ꂽ�I�u�W�F�N�g�̃^�O��Enemy�Ȃ�
        if(other.CompareTag("Enemy"))
        {
            //�Ώۂ��폜����
            Destroy(other.gameObject);
        }
    }
}
