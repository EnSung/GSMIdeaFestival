using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public string itemName;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().ownItem = this;

            transform.parent = collision.transform;
            transform.localPosition = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
