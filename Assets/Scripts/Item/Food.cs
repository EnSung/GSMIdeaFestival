using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : UsingItem
{
    public float up_gauge;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Use()
    {
        GameSceneManager.Instance.player.hungryGauge += up_gauge;

    }
}
