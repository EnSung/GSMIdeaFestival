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
        GetComponent<Collider2D>().enabled = true;

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

                GetComponent<Collider2D>().enabled = false;

            }
            else
            {
                UIManager.Instance.pop_UI("이미 사용아이템을 가지고있다.");
            }

        }
    }
}
