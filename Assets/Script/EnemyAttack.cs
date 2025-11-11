using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //エネミーに何かが接触した瞬間呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        //触れたオブジェクトのタグがPlayerなら...
        if(other.CompareTag("Player"))
        {

        }
    }
}
