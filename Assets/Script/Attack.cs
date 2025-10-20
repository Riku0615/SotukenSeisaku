using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //触れたオブジェクトのタグがEnemyなら
        if(other.CompareTag("Enemy"))
        {
            //対象を削除する
            Destroy(other.gameObject);
        }
    }
}
