using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : ScanningObject
{
    public GameObject targetObj;

    public Transform goalObj;


    private void OnTriggerEnter2D(Collider2D collision)
    {
            targetObj = collision.gameObject;

        if (collision.CompareTag("Player"))
        {
            GameSceneManager.Instance.playerTeleport(this.gameObject);
        }
            StartCoroutine(teleport());
    }



    public override void Scan(PlayerController player)
    {
        base.Scan(player);
        targetObj = player.gameObject;
        
        //StartCoroutine(teleport());
    }
    public virtual IEnumerator teleport()
    {

        yield return null;
        targetObj.transform.position = goalObj.transform.position;

    }
}
