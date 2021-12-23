using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskLamp : UsingItem
{

    public GameObject light;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }

    public override void Use()
    {
        Collider2D hits = Physics2D.OverlapCircle((Vector2)transform.position + GameSceneManager.Instance.player.dirVec, 0.3f);

        if (hits == null)
        {
            light.SetActive(true);
            gameObject.transform.parent = null;
            transform.position = (Vector2)transform.position + GameSceneManager.Instance.player.dirVec;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            GameSceneManager.Instance.player.usingItem = null;
            GetComponent<Collider2D>().enabled = true;

        }
    }
}
