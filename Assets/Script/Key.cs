using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool canGet = false;
    private Player player;

    [SerializeField]
    AudioClip getSE;    //Žæ“¾SE

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canGet = true;
            player = other.GetComponent<Player>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canGet = false;
            player = null;
        }
    }

    private void Update()
    {
        if(canGet && Input.GetKeyDown(KeyCode.Space)|| Input.GetButtonDown("Submit"))
        {
            player.KeyCount++;

            player.hasKey = true;
            PlayGetSE();
            Debug.Log("Œ®‚ðŽæ“¾‚µ‚½!");
            Destroy(gameObject,0.1f);
        }
    }

    void PlayGetSE()
    {
        if(getSE != null)
        {
            GameManager.PlaySE(getSE);
        }
    }
}
