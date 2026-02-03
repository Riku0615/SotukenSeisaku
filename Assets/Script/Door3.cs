using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3 : MonoBehaviour
{
    public Transform door;          //扉本体
    public float openSpeed = 2f;    //開く速度

    private bool canOpen = false;
    private bool isOpen = false;
    private Player player;

    [SerializeField]
    AudioClip openSE;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
            player = other.GetComponent<Player>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
            player = null;
        }
    }

    private void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
        {
            if (player != null && player.hasKey && !isOpen)
            {
                isOpen = true;
                PlayOpenSE();
                player.KeyCount--;
                Debug.Log("扉が開いた!");
            }
            else
            {
                Debug.Log("鍵が必要だ!");
            }
        }

        if (isOpen)
        {
            door.rotation = Quaternion.Lerp(
                door.rotation,
                Quaternion.Euler(0, 180, 0),
                Time.deltaTime * openSpeed
                );
        }
    }

    void PlayOpenSE()
    {
        if (openSE != null)
        {
            GameManager.PlaySE(openSE);
        }
    }
}
