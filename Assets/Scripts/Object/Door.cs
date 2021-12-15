using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Teleport
{
    public bool isLock;
    public string neededItemName;
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
                if (player.ownItem == null)
                {
                    
                }
                else
                {
                    if (player.ownItem.itemName == neededItemName)
                    {
                        if (player.ownItem != null)
                        {
                            isLock = false;
                            player.ownItem = null;
                        }
                    }
                }
                // 잠겨있다 등 텍스트 띄우기
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isLock)
        {
            targetObj = collision.gameObject;
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.CompareTag("Player"))
            {
                GameSceneManager.Instance.playerTeleport(this.gameObject);
            }
            StartCoroutine(teleport());
        }
    }
}   
