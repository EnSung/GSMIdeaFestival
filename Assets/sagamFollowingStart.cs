using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sagamFollowingStart : MonoBehaviour
{
    [SerializeField] GameObject sagam;
    [SerializeField] float speed;
    [SerializeField] bool flag;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {

            sagam.SetActive(true);
            sagam.transform.position = Vector2.Lerp(sagam.transform.position, new Vector2(GameSceneManager.Instance.player.transform.position.x -1, GameSceneManager.Instance.player.transform.position.y), speed* Time.deltaTime);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        flag = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            flag = true;
    }
}
