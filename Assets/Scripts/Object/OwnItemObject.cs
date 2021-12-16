
using UnityEngine;



public class OwnItemObject : ScanningObject
{

    public GameObject ownItem;


    private void Awake()
    {
            
    }

    public override void Scan(PlayerController player)
    {
        if(player.ownItem == null && ownItem != null){
            UIManager.Instance.pop_UI(ownItem.GetComponent<Item>().itemName + "¿ª(∏¶) »πµÊ«ﬂ¥Ÿ.");
            player.ownItem = ownItem.GetComponent<Item>();
            ownItem.transform.parent = player.transform;
            ownItem.transform.localPosition = Vector2.zero;
            ownItem = null;

        }
    }

    public void itemSpawn(GameObject obj)
    {
        ownItem = Instantiate(obj);
        ownItem.SetActive(false);
        ownItem.transform.parent = this.gameObject.transform;
        ownItem.transform.localPosition = Vector2.zero;
    }
}
