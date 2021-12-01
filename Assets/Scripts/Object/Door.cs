using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Teleport
{

    public bool isLock;

    public override void Scan(PlayerController player)
    {
        targetObj = player.gameObject;

        {

            if (!isLock)
            {
                    StartCoroutine(teleport());
            }
            else
            {
                if (player.ownItem.itemName == "열쇠")
                {
                    if (player.ownItem != null)
                    {
                        isLock = false;
                        player.ownItem = null;
                    }
                }
                // 잠겨있다 등 텍스트 띄우기
            }
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
