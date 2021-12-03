using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public List<GameObject> itemsPrefabs = new List<GameObject>();
    
    protected override void Awake()
    {
        
    }
}
