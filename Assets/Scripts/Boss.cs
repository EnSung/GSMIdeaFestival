using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject itemPrefab;

    float timer;
    public int cnt;

    public int maxHp;
    int Hp;
    public GameObject[] itemSpawnPos;
    bool flag;
    void Start()
    {
        Hp = maxHp;
        itemSpawnPos = GameObject.FindGameObjectsWithTag("itemSpawnPos");
        timer = Time.time + 3;
    }

    void Update()
    {
        if (GameSceneManager.Instance.isbossStart)
        {
            if (timer < Time.time)
            {
                Instantiate(bulletPrefab, transform.position,Quaternion.identity);
                timer = Time.time + 3;

                cnt++;
                flag = false;
            }
        }

        if(cnt % 10 == 0 && !flag)
        {
            //아이템 스폰
            int index = Random.Range(0, itemSpawnPos.Length);

            var obj =Instantiate(itemPrefab, itemSpawnPos[index].transform.position, Quaternion.identity);
            obj.GetComponent<Collider2D>().enabled = true;
            obj.GetComponent<SpriteRenderer>().enabled = true;

            flag = true;
        }
        
    }


    public void Damage()
    {
        Hp -= 1;
    }
}
