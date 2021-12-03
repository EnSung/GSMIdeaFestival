using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Teleport
{
   /* public enum Place
    {
        In,
        Out,
    }

    public Place place;*/
    public bool isLock;
    public string neededItemName;
    //public int floor;
    //public int address;
    public override void Scan(PlayerController player)
    {
        targetObj = player.gameObject;

        {

            if (!isLock)
            {
                StartCoroutine(teleport());

               /* if (place == Place.In)
                {
                    GameSceneManager.Instance.outRoom(this);
                }
                else
                {
                    GameSceneManager.Instance.inRoom(floor, address);
                }*/
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


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
