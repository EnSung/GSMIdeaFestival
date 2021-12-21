using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GameManager : Singleton<GameManager>
{

    public List<GameObject> itemsPrefabs = new List<GameObject>();

    public Dictionary<int, string> questDescription = new Dictionary<int, string>();
    protected override void Awake()
    {
        setJson();
    }


    void setJson()
    {
        TextAsset json = Resources.Load<TextAsset>("questDescription");

        Debug.Log(json.text);
        Debug.Log(1);

        JObject parsedObj = new JObject(); // Json Object »ý¼º

        Debug.Log(2);

        parsedObj = JObject.Parse(json.text); // Json Parsing
        Debug.Log(3);

        Debug.Log(parsedObj);


        foreach (KeyValuePair<string, JToken> pair in parsedObj)
        {
            questDescription.Add(System.Int32.Parse(pair.Key), pair.Value.ToString());
        }
    }
}
