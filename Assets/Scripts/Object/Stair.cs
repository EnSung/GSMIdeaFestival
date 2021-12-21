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
        if (GameSceneManager.Instance.questClearDict[floorNum])
        {
            isQuestClear = true;
        }    
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
                // 퀘스트 텍스트 바뀌기
                isFirst = false;

            }
        }
        else
        {
            UIManager.Instance.pop_UI("퀘스트를 완료해 주세요");
        }
    }
}
