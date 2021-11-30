public class OwnItemObject : ScanningObject
{

    public Item ownItem;
    public bool isNull;
    public override void Scan(PlayerController player)
    {
        if(player.ownItem == null && !isNull){
            player.ownItem = ownItem;
            ownItem = null;
            isNull = true;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
