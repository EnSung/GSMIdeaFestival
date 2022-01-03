using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GameManager : Singleton<GameManager>
{

    public GameObject ChooseItemPrefab;

    public List<GameObject> itemsPrefabs = new List<GameObject>();
    public List<Sprite> face = new List<Sprite>();
    public List<Sprite> bed = new List<Sprite>();
    public Dictionary<int, string> questDescription = new Dictionary<int, string>();

    public Queue<GameObject> UI_Q = new Queue<GameObject>();

    public Sprite sagamNormal;
    public Sprite sagamAngry;
    public Sprite sagamDie;


    [Header("music")]
    [Header("BGM")]
    public AudioClip normalGameMusic;
    public AudioClip BossMusic;
    public AudioClip LobbyMusic;
    public AudioClip FollowingMusic;


    [Header("SFX")]
    public AudioClip btnClickSFX;
    public AudioClip gameoverSFX;
    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        setJson();
        SceneManager.sceneLoaded += OnSceneLoaded;

        SoundManager.Instance.BgSoundPlay(GameManager.Instance.LobbyMusic);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UI_Q.Dequeue().SetActive(false);
        }
    }
    void setJson()
    {
        TextAsset json = Resources.Load<TextAsset>("questDescription");


        JObject parsedObj = new JObject(); // Json Object »ý¼º



        parsedObj = JObject.Parse(json.text); // Json Parsing
 
        Debug.Log(parsedObj);


        foreach (KeyValuePair<string, JToken> pair in parsedObj)
        {
            questDescription.Add(System.Int32.Parse(pair.Key), pair.Value.ToString());
        }

        foreach (var item in questDescription.Values)
        {
            Debug.Log(item);
        }
    }



    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UI_Q.Clear();
    }
}
