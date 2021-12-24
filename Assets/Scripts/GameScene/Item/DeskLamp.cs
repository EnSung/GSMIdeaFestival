using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskLamp : UsingItem
{

    bool isTimerStart;
    float timer;
    public float destroyTime;
    public GameObject light;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }

    private void Update()
    {
        if (isTimerStart)
        {
            timer += Time.deltaTime;
        }

        if(timer >= destroyTime)
        {
            Destroy(gameObject);
        }
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

            isTimerStart = true;
        }


    }
}