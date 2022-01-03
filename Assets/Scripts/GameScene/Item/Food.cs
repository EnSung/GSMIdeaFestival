using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : UsingItem
{
    public float up_gauge;
    public float time;
    void Start()
    {

    }

    void Update()
    {

    }

    public override void Use()
    {


        
            GameSceneManager.Instance.player.canMove_any = false;

        Invoke("eat", time);


    }

    public void eat()
    {

        GameSceneManager.Instance.player.canMove_any = true;
        GameSceneManager.Instance.player.hungryGauge += up_gauge;
        if(GameSceneManager.Instance.player.hungryGauge > 0){
            GameSceneManager.Instance.player.applySpeed = GameSceneManager.Instance.player.speed;   
        }

        GameSceneManager.Instance.player.usingItem = null;
        Destroy(gameObject);
    }


}
