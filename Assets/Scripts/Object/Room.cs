using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomNum;
    public Door door;
    public List<OwnItemObject> ownitemobjcts = new List<OwnItemObject>();

    private void Awake()
    {
        OwnItemObject[] obj = transform.GetComponentsInChildren<OwnItemObject>();

        foreach (var item in obj)
        {
            ownitemobjcts.Add(item);
        }
    }
}