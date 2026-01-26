using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool canGet = false;
    private Player player;

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
        if(canGet && Input.GetKeyDown(KeyCode.Space))
        {
            player.hasKey = true;
            Debug.Log("Œ®‚ðŽæ“¾‚µ‚½!");
            Destroy(gameObject);
        }
    }
}
