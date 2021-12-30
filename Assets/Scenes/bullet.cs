using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    Rigidbody2D rigid;
    [SerializeField] GameObject tar;
    public Vector2 targetVel;

    public float speed;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        rigid.velocity = (tar.transform.position - transform.position).normalized * speed;
    }

    void Update()
    {
        targetVel = rigid.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Vector2 inDir = Vector2.Reflect(targetVel, collision.contacts[0].normal);
            rigid.velocity = inDir;
        }
    }
}
