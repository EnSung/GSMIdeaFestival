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
            if (player.ownItem.itemName == "����")
            {
                isLock = false;
                player.ownItem = null;

            }
                // ����ִ� �� �ؽ�Ʈ ����
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
