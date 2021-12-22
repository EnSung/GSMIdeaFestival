using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : Item
{
    private void Update()
    {
        if (isGetByPlayer)
        {

            GameSceneManager.Instance.questClearDict[3] = true;
        }

    }
}
