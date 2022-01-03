using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiveFloorDoor : Door
{
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
                    if (item.itemName == neededItemName)
                    {
                        isLock = false;
                        UIManager.Instance.pop_UI(neededItemName + "�� ����ߴ�.");
                        GameSceneManager.Instance.questClearDict[5] = true;
                        return;
                    }
                }

                UIManager.Instance.pop_UI("���谡 ����.");



            }
            // ����ִ� �� �ؽ�Ʈ ����
        }
    }
    }


