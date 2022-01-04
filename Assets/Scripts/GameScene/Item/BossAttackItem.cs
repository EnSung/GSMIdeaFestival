using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackItem : UsingItem
{

    bool isFollowing;
    public override void Use()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        transform.parent = null;
        transform.position = (Vector2)transform.position + GameSceneManager.Instance.player.dirVec;
        isFollowing = true;
        GameSceneManager.Instance.player.usingItem = null;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector2.MoveTowards(transform.position, GameSceneManager.Instance.boss.transform.position, 5 * Time.deltaTime);
        }

    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isFollowing)
        {
            if (collision.CompareTag("Player"))
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

            }
        }
        
        if (collision.gameObject.CompareTag("boss"))
        {
            GameSceneManager.Instance.boss.Damage();
            Destroy(gameObject);
        }
    }

    public void lookat2D(Vector3 target)
    {

        Vector3 dir = (target - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
