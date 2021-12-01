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
                if (player.ownItem.itemName == "����")
                {
                    if (player.ownItem != null)
                    {
                        isLock = false;
                        player.ownItem = null;
                    }
                }
                // ����ִ� �� �ؽ�Ʈ ����
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
