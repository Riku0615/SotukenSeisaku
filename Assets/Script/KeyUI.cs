using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyUI : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI hintText;

    public int needKeyCount = 1; //必要な鍵の数

    void Update()
    {
        hintText.text = 
            "扉を開く方法\n" +
            "鍵を取得: " + player.KeyCount + "/" + needKeyCount;
    }
}
