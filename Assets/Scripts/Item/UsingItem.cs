using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingItem : Item
{


    void Start()
    {
        type = define.itemType.usingItem;
        itemImage = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public virtual void Use()
    {
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.GetComponent<PlayerController>().usingItem == null)
            {
                collision.GetComponent<PlayerController>().usingItem = this;
                this.isGetByPlayer = true;
                transform.parent = collision.transform;
                transform.localPosition = Vector2.zero;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
           
        }
    }
}
