using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskLamp : UsingItem
{

    bool isTimerStart;
    float timer;

    bool isCreate;
    public GameObject light;
    public override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (!isCreate)
            {
                if (collision.GetComponent<PlayerController>().usingItem == null)
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

    private void Update()
    {

    }
    public override void Use()
    {
        Collider2D hits = Physics2D.OverlapCircle((Vector2)transform.position + GameSceneManager.Instance.player.dirVec, 0.3f);

        if (hits == null)
        {
            isCreate = true;
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
