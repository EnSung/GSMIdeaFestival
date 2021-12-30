using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPhoneBag : GaugingObject
{

    SmartPhone smartPhone;
    [SerializeField] GameObject smartPhonePrefab;
    

    protected override void Start()
    {
        base.Start();
        smartPhone = Instantiate(smartPhonePrefab).GetComponent<SmartPhone>();
        smartPhone.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        smartPhone.gameObject.GetComponent<Collider2D>().enabled = false;
        smartPhone.gameObject.transform.parent = transform;
        smartPhone.gameObject.transform.localPosition = Vector2.zero;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void completingPlayFunc()
    {
        base.completingPlayFunc();
        GameSceneManager.Instance.player.ownItemList.Add(smartPhone);
        smartPhone.gameObject.transform.parent = GameSceneManager.Instance.player.transform;
        smartPhone.gameObject.transform.localPosition = Vector2.zero;
        smartPhone.isGetByPlayer = true;
        UIManager.Instance.pop_UI("스마트폰을 얻었다.");
        smartPhone = null;

        

    }

    public override void Scan(PlayerController player)
    {
        base.Scan(player);
    }
}
