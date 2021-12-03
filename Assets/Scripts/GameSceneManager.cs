using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : Singleton<GameSceneManager>
{

    public Dictionary<int, List<Room>> rooms = new Dictionary<int, List<Room>>();
    public Dictionary<int, List<Door>> doors = new Dictionary<int, List<Door>>();

    void Start()
    {
        rooms[3] = new List<Room>();
        doors[3] = new List<Door>();


        Room[] roms = GameObject.Find("Floor_3").transform.FindChild("Room").GetComponentsInChildren<Room>();
        Door[] dors = GameObject.Find("Floor_3").transform.FindChild("Door").GetComponentsInChildren<Door>();

        foreach (Room room in roms)
        {
            rooms[3].Add(room);
        }

        foreach (Door door in dors)
        {
            doors[3].Add(door);
        }
        foreach (var i in rooms[3])
        {
            Debug.Log(i.name);
        }

        mapSetting();
    }


    public void mapSetting()
    {
        

        for (int i = 0; i < 3; i++)
        {

            for (int j = 0; j < 20; j++)
            {
                rooms[3][i].door.goalObj = doors[3][i].transform.FindChild("startPos");
                doors[3][i].goalObj = rooms[3][i].door.transform.FindChild("startPos");
            }
            int floorNum = Random.Range(0, 5);
            int roomNum = Random.Range(0, 20);
            int objNum = Random.Range(0, 4);

            Debug.Log(roomNum + "   " + objNum); 
            rooms[3][roomNum].ownitemobjcts[objNum].itemSpawn(GameManager.Instance.itemsPrefabs[0]);

            Debug.Log(rooms[3][roomNum].ownitemobjcts[objNum].transform.position.x + "," + rooms[3][roomNum].ownitemobjcts[objNum].transform.position.y);
            Debug.Log(rooms[3][roomNum].ownitemobjcts[objNum].ownItem.name);
        }
    }
}