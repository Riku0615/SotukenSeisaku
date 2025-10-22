using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFade : MonoBehaviour
{
    public Transform player;//プレイヤーを指定
    private Renderer lastHitRenderer;
    private Color lastColor;

    void Update()
    {
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;

        if(Physics.Raycast(transform.position, direction,out hit))
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();

            if(renderer != null)
            {
                if(lastHitRenderer != renderer)
                {
                    ResetTransparency();//以前の壁を元に戻す
                    lastHitRenderer = renderer;
                    lastColor = renderer.material.color;

                    Color newColor = lastColor;
                    newColor.a = 0.5f;
                    renderer.material.color = newColor;
                }
            }
        }
        else
        {
            ResetTransparency();
        }
    }

    void ResetTransparency()
    {
        if(lastHitRenderer !=null)
        {
            lastHitRenderer.material.color = lastColor;
            lastHitRenderer = null;
        }
    }
}
