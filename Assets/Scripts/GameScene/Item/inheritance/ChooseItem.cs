using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseItem : Item
{

    public static GameObject choosingItem;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        GameSceneManager.Instance.ItemChoose = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (GameSceneManager.Instance.ItemChoose)
        {
            if(choosingItem != this.gameObject)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
