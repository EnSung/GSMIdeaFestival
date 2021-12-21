using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : Teleport
{

    public bool isFirst;
    public int floorNum;
    public bool Lock;

    public override void Scan(PlayerController player)
    {
        base.Scan(player);
    }

    public override IEnumerator teleport()
    {
        return base.teleport();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Lock)
        {
            if (collision.CompareTag("Player"))
            {
                UIManager.Instance.pop_UI(goalObj.gameObject.GetComponentInParent<Teleport>().name);
                GameSceneManager.Instance.playerTeleport(this.gameObject.GetComponent<Teleport>());
            }

            StartCoroutine(teleport());
        }
        else
        {

        }
    }
}
