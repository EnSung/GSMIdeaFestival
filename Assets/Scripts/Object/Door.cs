using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Teleport
{

    public bool isLock;

    public override void Scan(PlayerController player)
    {
        targetObj = player.gameObject;
        if (!isLock)
        {
            StartCoroutine(teleport());
        }
        else
        {
            if (player.ownItem.itemName == "열쇠")
            {
                isLock = false;
                player.ownItem = null;

            }
                // 잠겨있다 등 텍스트 띄우기
        }
    }

    public override string ToString()
    {
        return base.ToString();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
