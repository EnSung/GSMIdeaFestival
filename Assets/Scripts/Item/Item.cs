using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using define;
public class Item : MonoBehaviour
{

    public SpriteRenderer itemImage;
    public string itemName;



    public itemType type;
    void Start()
    {
        type = itemType.staticItem;
        itemImage = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().ownItemList.Add(this);

            transform.parent = collision.transform;
            transform.localPosition = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
