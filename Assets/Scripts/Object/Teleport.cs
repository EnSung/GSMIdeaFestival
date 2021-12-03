using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : ScanningObject
{
    public GameObject targetObj;

    public Transform goalObj;

    bool canTeleport;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetObj = collision.gameObject;
            canTeleport = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            canTeleport = false;
    }

    public override void Scan(PlayerController player)
    {
        base.Scan(player);
        targetObj = player.gameObject;
        StartCoroutine(teleport());
    }
    public virtual IEnumerator teleport()
    {

        yield return null;
        targetObj.transform.position = goalObj.transform.position;

    }
}
