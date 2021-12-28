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

                    UIManager.Instance.pop_UI("���谡 ����.");
                }
                else
                {
                    foreach (Item item in player.ownItemList)
                    {
                        if(item.itemName == neededItemName)
                        {
                                isLock = false;
                                player.ownItemList.Remove(item);
                            UIManager.Instance.pop_UI(neededItemName + "�� ����ߴ�.");
                            return;
                        }
                    }

                UIManager.Instance.pop_UI("���谡 ����.");



            }
            // ����ִ� �� �ؽ�Ʈ ����
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
            UIManager.Instance.pop_UI("����ִ�.");

        }
    }
}   
