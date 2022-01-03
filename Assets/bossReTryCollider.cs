using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossReTryCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            GameSceneManager.Instance.isBossRetry = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
            GameSceneManager.Instance.isBossRetry = true;
    }
}
