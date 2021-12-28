
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

        Debug.Log("ownItemObject Scan");
        if(ownItem != null)
        {
            if (ownItem.GetComponent<Item>().type == itemType.staticItem)
            {
                if (ownItem != null)
                {
                    UIManager.Instance.pop_UI(ownItem.GetComponent<Item>().itemName + "을(를) 획득했다.");
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
                    UIManager.Instance.pop_UI(ownItem.GetComponent<Item>().itemName + "을(를) 획득했다.");
                    player.usingItem = ownItem.GetComponent<UsingItem>();
                    ownItem.transform.parent = player.transform;
                    ownItem.transform.localPosition = Vector2.zero;
                    ownItem.GetComponent<UsingItem>().isGetByPlayer = true;

                    ownItem = null;
                }


            }
        }
        else
        {
            UIManager.Instance.pop_UI("아무것도 들어있지않다.");
        }
        
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
