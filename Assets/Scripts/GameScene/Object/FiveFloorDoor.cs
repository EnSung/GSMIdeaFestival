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

                UIManager.Instance.pop_UI("열쇠가 없다.");
            }
            else
            {
                foreach (Item item in player.ownItemList)
                {
                    if (item.itemName == neededItemName)
                    {
                        isLock = false;
                        UIManager.Instance.pop_UI(neededItemName + "을 사용했다.");
                        GameSceneManager.Instance.questClearDict[5] = true;
                        return;
                    }
                }

                UIManager.Instance.pop_UI("열쇠가 없다.");



            }
            // 잠겨있다 등 텍스트 띄우기
        }
    }
    }


