using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using define;
public class Item : MonoBehaviour
{

    public bool isGetByPlayer;
    public SpriteRenderer itemImage;
    public string itemName;



    public itemType type;
    void Start()
    {
        type = itemType.staticItem;
        itemImage = GetComponent<SpriteRenderer>();
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().ownItemList.Add(this);
            
            this.isGetByPlayer = true;

            transform.parent = collision.transform;
            transform.localPosition = Vector2.zero;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
