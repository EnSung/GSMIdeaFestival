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
                if (player.ownItemList == null)
                {
                    
                }
                else
                {
                    foreach (Item item in player.ownItemList)
                    {
                        if(item.itemName == neededItemName)
                        {
                                isLock = false;
                                player.ownItemList.Remove(item);
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
            if (collision.collider.CompareTag("Player"))
            {
                targetObj = collision.gameObject;

            }
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.CompareTag("Player"))
            {
                UIManager.Instance.pop_UI(goalObj.GetComponentInParent<Teleport>().name);
                GameSceneManager.Instance.playerTeleport(this.gameObject.GetComponent<Teleport>());
            }
            StartCoroutine(teleport());
        }
    }
}   
