using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : Singleton<GameSceneManager>
{

    public Dictionary<int, Dictionary<int, Room>> rooms = new Dictionary<int, Dictionary<int, Room>>();
    public Dictionary<int, Dictionary<int, Door>> doors = new Dictionary<int, Dictionary<int, Door>>();

    public GameObject playerTeleportObject;
    public bool isTeleport;
    void Start()
    {
        rooms[3] = new Dictionary<int, Room>();
        doors[3] = new Dictionary<int, Door>();


        Room[] roms = GameObject.Find("Floor_3").transform.Find("Room").GetComponentsInChildren<Room>();
        Door[] dors = GameObject.Find("Floor_3").transform.Find("Door").GetComponentsInChildren<Door>();



        foreach (Room room in roms)
        {
            rooms[3].Add(System.Int32.Parse(room.name),room);
        }

        foreach (Door door in dors)
        {
            doors[3].Add(System.Int32.Parse(door.name),door);
        }



        mapSetting();
    }


    public void mapSetting()
    {
        

        for (int i = 3; i <= 5; i++)
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

            int floorNum = Random.Range(3, 4);
            int roomNum = Random.Range(0, 20);
            int objNum = Random.Range(0, 4);

            int total = (roomNum < 10) ? System.Int32.Parse(floorNum.ToString() + '0' + roomNum.ToString()) : System.Int32.Parse(floorNum.ToString() + roomNum.ToString());

            Debug.Log(total + "   " + objNum);
            rooms[3][total].ownitemobjcts[objNum].itemSpawn(GameManager.Instance.itemsPrefabs[0]);

        }
    }

    public void playerTeleport(GameObject go)
    {
        playerTeleportObject = go;

        isTeleport = true;

    }
}