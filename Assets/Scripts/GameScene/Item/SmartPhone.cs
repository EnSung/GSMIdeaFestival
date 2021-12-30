using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPhone : Item
{
    void Update()
    {
        if (isGetByPlayer)
        {

            GameSceneManager.Instance.questClearDict[3] = true;
        }
    }
}
