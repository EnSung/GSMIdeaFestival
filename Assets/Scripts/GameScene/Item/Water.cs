using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    float speed;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        speed = collision.gameObject.GetComponent<Unit>().applySpeed;
        collision.gameObject.GetComponent<Unit>().applySpeed = 2;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        collision.gameObject.GetComponent<Unit>().applySpeed = 2;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name + "exit");

      collision.gameObject.GetComponent<Unit>().applySpeed = speed;
    }
}
