using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hood : ChooseItem
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        GameSceneManager.Instance.player.hp += 1;

        UIManager.Instance.pop_UI(itemName + "�� ȹ���ߴ�." +
            "HP�� 1 �����ߴ�.");
    }
}
