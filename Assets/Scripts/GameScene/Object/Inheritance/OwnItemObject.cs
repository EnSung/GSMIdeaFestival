
using UnityEngine;
using define;


public class OwnItemObject : ScanningObject
{


    
    public GameObject ownItem;


    private void Awake()
    {
    }

    public override void Scan(PlayerController player)
    {

        if(ownItem != null)
        {
            if (ownItem.GetComponent<Item>().type == itemType.staticItem)
            {
                if (ownItem != null)
                {
                    UIManager.Instance.pop_UI(ownItem.GetComponent<Item>().itemName + "��(��) ȹ���ߴ�.");
                    player.ownItemList.Add(ownItem.GetComponent<Item>());
                    ownItem.transform.parent = player.transform;
                    ownItem.transform.localPosition = Vector2.zero;
                    ownItem.GetComponent<Item>().isGetByPlayer = true;
                    ownItem = null;
                }

            }
            else
            {
                if (player.usingItem == null && ownItem != null)
                {
                    UIManager.Instance.pop_UI(ownItem.GetComponent<Item>().itemName + "��(��) ȹ���ߴ�.");
                    player.usingItem = ownItem.GetComponent<UsingItem>();
                    ownItem.transform.parent = player.transform;
                    ownItem.transform.localPosition = Vector2.zero;
                    ownItem.GetComponent<UsingItem>().isGetByPlayer = true;

                    ownItem = null;
                }
                else if(player.usingItem)
                {
                    UIManager.Instance.pop_UI(ownItem.GetComponent<Item>().itemName + "�� ������ �̹� ���� �������ִ�.");
                }


            }
        }
        else
        {
            UIManager.Instance.pop_UI("�ƹ��͵� ��������ʴ�.");
        }
        
    }

    public void itemRandom()
    {
        int range = Random.Range(1, 101);
        int index;

        if(range <= 80 || ownItem != null)
        {
            return;
        }
        else if(range >= 81 && range <= 90)
        {
            index = 2;
        }
        else if(range >= 91 && range <= 95)
        {
            index = 3;
        }
        else
        {
            index = 4;
        }

        itemSpawn(GameManager.Instance.itemsPrefabs[index]);
    }
    public void itemSpawn(GameObject obj)
    {
        ownItem = Instantiate(obj);
        ownItem.GetComponent<SpriteRenderer>().enabled = false;
        ownItem.GetComponent<Collider2D>().enabled = false;
        ownItem.transform.parent = this.gameObject.transform;
        ownItem.transform.localPosition = Vector2.zero;
    }
}
