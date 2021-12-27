using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBasket : UsingItem
{
    bool isTimerStart;
    float timer;

    public float destroyTime;
    public Water water;
    private void Awake()
    {
        water = GetComponentInChildren<Water>();
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
        Collider2D hits = Physics2D.OverlapCircle((Vector2)transform.position + (GameSceneManager.Instance.player.dirVec * -1), 0.3f);

        if (hits == null)
        {
            water.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            water.gameObject.GetComponent<Collider2D>().enabled = true;
            Debug.Log(1);
            gameObject.transform.parent = null;
            Debug.Log(2);
            gameObject.transform.position = (Vector2)transform.position + (GameSceneManager.Instance.player.dirVec * -1);
            Debug.Log(3);
            itemImage.enabled = true;
            Debug.Log(4);
            GetComponent<Collider2D>().enabled = true;
            Debug.Log(5);
            GameSceneManager.Instance.player.usingItem = null;
            Debug.Log(6);
            isTimerStart = true;
        }
    }

  


}
