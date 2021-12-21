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
            collision.GetComponent<PlayerController>().usingItem = this;
            collision.GetComponent<Item>().isGetByPlayer = true;

            transform.parent = collision.transform;
            transform.localPosition = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
