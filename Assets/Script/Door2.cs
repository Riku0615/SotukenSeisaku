using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    public Transform door;          //扉本体
    public float openSpeed = 2f;    //開く速度

    private bool canOpen = false;
    private bool isOpen = false;
    private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canOpen = true;
            player = other.GetComponent<Player>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canOpen = false;
            player = null;
        }
    }

    private void Update()
    {
        if(canOpen && Input.GetKeyDown(KeyCode.Space))
        {
            if(player != null && player.hasKey)
            {
                isOpen = true;
                Debug.Log("扉が開いた!");
            }
            else
            {
                Debug.Log("鍵が必要だ!");
            }
        }

        if(isOpen)
        {
            door.rotation = Quaternion.Lerp(
                door.rotation,
                Quaternion.Euler(0, 0, 0),
                Time.deltaTime * openSpeed
                );
        }
    }
}
