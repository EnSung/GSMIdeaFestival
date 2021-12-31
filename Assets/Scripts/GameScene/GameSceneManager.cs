using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : Singleton<GameSceneManager>
{

    public List<int> ItemOwnList = new List<int>();
    public Dictionary<int, Dictionary<int, Room>> rooms = new Dictionary<int, Dictionary<int, Room>>();
    public Dictionary<int, Dictionary<int, Door>> doors = new Dictionary<int, Dictionary<int, Door>>();
    public Dictionary<int, bool> questClearDict = new Dictionary<int, bool>();
    public Teleport playerTeleportObject;
    public Teleport prev_playerTeleportObject;
    public bool isTeleport;

    public bool isGameover;

    public PlayerController player;
    public Boss boss;
    GameObject[] itemDrop;

    [Header("boss")]
    #region boss
    public GameObject bossStartBang;
    public GameObject mainCanvas;
    public GameObject bossCanvas;

    public Transform teleportPos;

    public bool isbossStart;
    #endregion
    protected override void Awake()
    {
        for (int i = 1; i <= 5; i++)
        {
            questClearDict.Add(i, false);
        }
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        linkMap();
        mapSetting();

    }


    public void linkMap()
    {
        rooms[3] = new Dictionary<int, Room>();
        rooms[4] = new Dictionary<int, Room>();
        rooms[5] = new Dictionary<int, Room>();
        doors[3] = new Dictionary<int, Door>();
        doors[4] = new Dictionary<int, Door>();
        doors[5] = new Dictionary<int, Door>();

        Room[] roms_3 = GameObject.Find("Floor_3").transform.Find("Room").GetComponentsInChildren<Room>();
        Door[] dors_3 = GameObject.Find("Floor_3").transform.Find("Door").GetComponentsInChildren<Door>();
        Room[] roms_4 = GameObject.Find("Floor_4").transform.Find("Room").GetComponentsInChildren<Room>();
        Door[] dors_4 = GameObject.Find("Floor_4").transform.Find("Door").GetComponentsInChildren<Door>();
        Room[] roms_5 = GameObject.Find("Floor_5").transform.Find("Room").GetComponentsInChildren<Room>();
        Door[] dors_5 = GameObject.Find("Floor_5").transform.Find("Door").GetComponentsInChildren<Door>();
        foreach (Room room in roms_3)
        {
            rooms[3].Add(System.Int32.Parse(room.gameObject.name), room);
        }
        foreach (Door door in dors_3)
        {
            doors[3].Add(System.Int32.Parse(door.gameObject.name), door);
        }

        foreach (Room room in roms_4)
        {
            rooms[4].Add(System.Int32.Parse(room.gameObject.name), room);
        }
        foreach (Door door in dors_4)
        {
            doors[4].Add(System.Int32.Parse(door.gameObject.name), door);
        }

        foreach (Room room in roms_5)
        {
            rooms[5].Add(System.Int32.Parse(room.gameObject.name), room);
        }
        foreach (Door door in dors_5)
        {
            doors[5].Add(System.Int32.Parse(door.gameObject.name), door);
        }



        for (int i = 3; i <= 4; i++)
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
        }
    }
    public void mapSetting()
    {


        #region ÁÖ¼®

        int total = 0, floorNum = 0, roomNum = 0, objNum = 0;
        /*
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

        randomNum(ref floorNum, ref roomNum, ref total, ref objNum);

        Debug.Log(total + "   " + objNum);

        if (rooms[3][total].ownitemobjcts[objNum].ownItem == null)
        {
            rooms[3][total].ownitemobjcts[objNum].itemSpawn(GameManager.Instance.itemsPrefabs[4]);

        }
        randomNum(ref floorNum, ref roomNum, ref total, ref objNum);

        Debug.Log(total + "   " + objNum);

        if (rooms[3][total].ownitemobjcts[objNum].ownItem == null)
        {
            rooms[3][total].ownitemobjcts[objNum].itemSpawn(GameManager.Instance.itemsPrefabs[0]);

        }*/

        #endregion

        GameObject[] faces = GameObject.FindGameObjectsWithTag("face");

        int faceIndex;
        foreach (var item in faces)
        {
            faceIndex = Random.Range(0, GameManager.Instance.face.Count);
            item.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.face[faceIndex];
        }

        GameObject[] beds = GameObject.FindGameObjectsWithTag("bed");
        int bedIndex;

        foreach (var item in beds)
        {
            bedIndex = Random.Range(0, GameManager.Instance.bed.Count);
            item.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.bed[bedIndex];
        }
        itemDrop = GameObject.FindGameObjectsWithTag("itemDrop");

        int index = 0;

        bool flag;


        randomNum(4, ref roomNum, ref total, ref objNum);


        Debug.Log(total + "   " + objNum);

        rooms[4][total].ownitemobjcts[objNum].itemSpawn(GameManager.Instance.itemsPrefabs[2]);


        randomNum(3, ref roomNum, ref total, ref objNum);


        Debug.Log(total + "   " + objNum);

        rooms[3][total].ownitemobjcts[objNum].itemSpawn(GameManager.Instance.itemsPrefabs[0]);
        for (int i = 0; i < 10; i++)
        {

            flag = false;
            while (!flag)
            {
                index = Random.Range(0, itemDrop.Length);

                if (itemDrop[index].GetComponent<OwnItemObject>().ownItem == null)
                {


                    flag = true;
                }
            }
            Debug.Log("itemDrop in " + itemDrop[index].transform.parent.name + " / " + itemDrop[index].name);
            itemDrop[index].GetComponent<OwnItemObject>().itemSpawn(GameManager.Instance.itemsPrefabs[1]);

        }






    }

    public void playerTeleport(Teleport go)
    {
        isTeleport = true;
        playerTeleportObject = go;
        if (prev_playerTeleportObject == null)
            prev_playerTeleportObject = go.goalObj.parent.gameObject.GetComponent<Teleport>();
    }

    public void randomNum(ref int floorNum, ref int roomNum, ref int total, ref int objNum)
    {

        floorNum = Random.Range(3, 4);
        roomNum = Random.Range(1, 20);

        total = (roomNum < 10) ? System.Int32.Parse(floorNum.ToString() + '0' + roomNum.ToString()) : System.Int32.Parse(floorNum.ToString() + roomNum.ToString());

        objNum = Random.Range(0, rooms[3][total].ownitemobjcts.Count);

    }

    public void randomNum(int floorNum, ref int roomNum, ref int total, ref int objNum)
    {

        roomNum = Random.Range(1, 20);

        total = (roomNum < 10) ? System.Int32.Parse(floorNum.ToString() + '0' + roomNum.ToString()) : System.Int32.Parse(floorNum.ToString() + roomNum.ToString());

        objNum = Random.Range(0, rooms[floorNum][total].ownitemobjcts.Count);

    }

    public void bossStart()
    {
        StartCoroutine(bossStartCoroutine());
    }

    public void bossEnd()
    {
        StartCoroutine(bossEndCoroutine());
    }

    IEnumerator bossStartCoroutine()
    {
        yield return null;

        bossStartBang.SetActive(true);

        mainCanvas.SetActive(false);
        player.light.pointLightOuterRadius = 30f;

        player.transform.position = teleportPos.position;

        isbossStart = true;
    }



    IEnumerator bossEndCoroutine()
    {
        yield return null;

        bossStartBang.SetActive(true);

        mainCanvas.SetActive(false);
        player.light.pointLightOuterRadius = 30f;

        player.transform.position = teleportPos.position;

        isbossStart = true;
    }

}
