using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoes : ChooseItem
{
    bool isget;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        isget = true;

        UIManager.Instance.pop_UI(itemName + "�� ȹ���ߴ�." +
            "�̵��ӵ��� �����ƴ�.");
    }

    void Update()
    {
        if (isget)
        {

            GameSceneManager.Instance.player.applySpeed = 8;
        }
    }
}
