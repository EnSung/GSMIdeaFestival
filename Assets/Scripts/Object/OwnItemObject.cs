
using UnityEngine;



public class OwnItemObject : ScanningObject
{

    public GameObject ownItem;
    public override void Scan(PlayerController player)
    {
        if(player.ownItem == null && ownItem != null){
            player.ownItem = ownItem.GetComponent<Item>();
            ownItem = null;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
