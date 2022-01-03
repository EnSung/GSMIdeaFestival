using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    float speed;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        speed = collision.gameObject.GetComponent<Unit>().applySpeed;

        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Unit>().applySpeed = 2;

        }
        else
        {
            collision.gameObject.GetComponentInChildren<Unit>().applySpeed = 2;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Unit>().applySpeed = 2;

        }
        else
        {
            collision.gameObject.GetComponentInChildren<Unit>().applySpeed = 2;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name + "exit");dd
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Unit>().applySpeed = speed;

        }
        else
        {
            collision.gameObject.GetComponentInChildren<Unit>().applySpeed = speed;
        }
    }
}
