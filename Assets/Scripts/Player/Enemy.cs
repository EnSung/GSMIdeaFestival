using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isFollowing;

    GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 5 * Time.deltaTime);
        }

    }
}
