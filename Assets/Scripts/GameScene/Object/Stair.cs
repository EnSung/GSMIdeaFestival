using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : Teleport
{

    PlayerController player;
    public bool isFirst;
    public int floorNum;
    public int goTofloorNum;
    public bool isQuestClear;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {

       

    }

    public override void Scan(PlayerController player)
    {
        base.Scan(player);
    }

    public override IEnumerator teleport()
    {
        return base.teleport();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameSceneManager.Instance.questClearDict[floorNum])
        {
            isQuestClear = true;
        }


        if (isQuestClear)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                UIManager.Instance.pop_UI(goalObj.gameObject.GetComponentInParent<Teleport>().name);
                GameSceneManager.Instance.playerTeleport(this.gameObject.GetComponent<Teleport>());
                targetObj = collision.gameObject;
                StartCoroutine(teleport());

            }


            player.curFloor = goTofloorNum;
            if (isFirst)
            {
                UIManager.Instance.questTextUpdate(goTofloorNum);
                player.curQuestFloor = goTofloorNum;
                isFirst = false;

            }
        }
        else
        {
            UIManager.Instance.pop_UI("아직 할 일이 남아있다.");
        }
    }
}
