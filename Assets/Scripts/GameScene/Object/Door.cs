using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Teleport
{
    public bool isLock;
    public string neededItemName;
    bool flag;
    public override void Scan(PlayerController player)
    {

            if (isLock)
            {

                if (player.ownItemList == null)
                {

                    UIManager.Instance.pop_UI("열쇠가 없다.");
                }
                else
                {
                    foreach (Item item in player.ownItemList)
                    {
                        if(item.itemName == neededItemName)
                        {
                                isLock = false;
                                player.ownItemList.Remove(item);
                            UIManager.Instance.pop_UI(neededItemName + "을 사용했다.");
                            return;
                        }
                    }

                UIManager.Instance.pop_UI("열쇠가 없다.");



            }
            // 잠겨있다 등 텍스트 띄우기
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isLock)
        {
            targetObj = collision.gameObject;

            if (collision.gameObject.CompareTag("Player"))
            {
                UIManager.Instance.pop_UI(goalObj.GetComponentInParent<Teleport>().name);
                GameSceneManager.Instance.playerTeleport(this.gameObject.GetComponent<Teleport>());
            }
            StartCoroutine(teleport());
        }
        else
        {
            UIManager.Instance.pop_UI("잠겨있다.");

        }
    }
}   
