using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : Singleton<GameSceneManager>
{

    public Dictionary<int, Dictionary<int, Room>> rooms = new Dictionary<int, Dictionary<int, Room>>();
    public Dictionary<int, Dictionary<int, Door>> doors = new Dictionary<int, Dictionary<int, Door>>();
    public Dictionary<int, bool> questClearDict = new Dictionary<int, bool>();
    public Teleport playerTeleportObject;
    public Teleport prev_playerTeleportObject;
    public bool isTeleport;

    public bool isGameover;

    public PlayerController player;
    protected override void Awake()
    {


        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        rooms[3] = new Dictionary<int, Room>();
        doors[3] = new Dictionary<int, Door>();


        Room[] roms = GameObject.Find("Floor_3").transform.Find("Room").GetComponentsInChildren<Room>();
        Door[] dors = GameObject.Find("Floor_3").transform.Find("Door").GetComponentsInChildren<Door>();



        foreach (Room room in roms)
        {
            rooms[3].Add(System.Int32.Parse(room.gameObject.name),room);
        }

        foreach (Door door in dors)
        {
            doors[3].Add(System.Int32.Parse(door.gameObject.name),door);
        }


        for (int i = 1; i <= 5; i++)
        {
            questClearDict.Add(i, false);
        }



        mapSetting();
    }

    private void Update()
    {
        //Debug.Log("isTel    "+isTeleport);
    }
    public void mapSetting()
    {
        

        for (int i = 3; i <= 3; i++)
        {

            for (int j = 1; j <= 20; j++)
            {
                int num = (j < 10) ? System.Int32.Parse(i.ToString() + '0' + j.ToString()) : System.Int32.Parse(i.ToString() + j.ToString());

                Debug.Log(num);
                rooms[i][num].door.goalObj =
                    doors[i][num].transform.Find("startPos").transform;
                doors[i][num].goalObj = 
                    rooms[i][num].door.transform.Find("startPos").transform;    

                
            }
            int total = 0, floorNum=0,roomNum=0, objNum=0;

            randomNum(ref floorNum, ref roomNum, ref total, ref objNum);

            Debug.Log(total + "   " + objNum);
            if(rooms[3][total].ownitemobjcts[objNum].ownItem == null)
            {
                rooms[3][total].ownitemobjcts[objNum].itemSpawn(GameManager.Instance.itemsPrefabs[1]);

            }

            roomNum = Random.Range(1, 20);


            objNum = Random.Range(0, rooms[3][total].ownitemobjcts.Count);

            total = (roomNum < 10) ? System.Int32.Parse(floorNum.ToString() + '0' + roomNum.ToString()) : System.Int32.Parse(floorNum.ToString() + roomNum.ToString());
            
            Debug.Log(total + "   " + objNum);

            rooms[3][total].ownitemobjcts[objNum].itemSpawn(GameManager.Instance.itemsPrefabs[2]);

            randomNum(ref floorNum, ref roomNum, ref total, ref objNum);

            Debug.Log(total + "   " + objNum);

            if (rooms[3][total].ownitemobjcts[objNum].ownItem == null)
            {
                rooms[3][total].ownitemobjcts[objNum].itemSpawn(GameManager.Instance.itemsPrefabs[3]);

            }

        }
    }

    public void playerTeleport(Teleport go)
    {
        isTeleport = true;
        playerTeleportObject = go;
        if(prev_playerTeleportObject == null)
            prev_playerTeleportObject = go.goalObj.parent.gameObject.GetComponent<Teleport>();
    }

    public void randomNum(ref int floorNum,ref int roomNum,ref int total, ref int objNum)
    {

        floorNum = Random.Range(3, 4);
        roomNum = Random.Range(1, 20);

        total = (roomNum < 10) ? System.Int32.Parse(floorNum.ToString() + '0' + roomNum.ToString()) : System.Int32.Parse(floorNum.ToString() + roomNum.ToString());

        objNum = Random.Range(0, rooms[3][total].ownitemobjcts.Count);

    }

    public void randomNum(int floorNum,ref int roomNum, ref int total, ref int objNum)
    {

        roomNum = Random.Range(1, 20);

        total = (roomNum < 10) ? System.Int32.Parse(floorNum.ToString() + '0' + roomNum.ToString()) : System.Int32.Parse(floorNum.ToString() + roomNum.ToString());

        objNum = Random.Range(0, rooms[3][total].ownitemobjcts.Count);

    }



}   