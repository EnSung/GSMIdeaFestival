using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject itemPrefab;

    float timer;
    public int cnt;
    public int cntTab;

    public bool clear_flag;
    public int maxHp;
    [SerializeField] public int Hp;
    public GameObject[] itemSpawnPos;
    bool flag;

    public bool isDie;
    public Queue<GameObject> bullets = new Queue<GameObject>();
    [SerializeField] Slider Hp_UI;
    void Awake()
    {
        Hp = maxHp;
        Hp_UI.maxValue = this.maxHp;
        itemSpawnPos = GameObject.FindGameObjectsWithTag("itemSpawnPos");
        timer = Time.time + 3;
    }

    void Update()
    {

        if(Hp <= 0)
        {
            Die();
        }
        Boss_HP_Update();

        if (!isDie)
        {
            if (GameSceneManager.Instance.isbossStart)
            {
                if (timer < Time.time)
                {
                    var obj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    bullets.Enqueue(obj);

                    timer = Time.time + 3;

                    cnt++;
                    flag = false;
                }
            }

            if (cnt % cntTab == 0 && !flag)
            {
                //아이템 스폰
                int index = Random.Range(0, itemSpawnPos.Length);

                var obj = Instantiate(itemPrefab, itemSpawnPos[index].transform.position, Quaternion.identity);
                obj.GetComponent<Collider2D>().enabled = true;
                obj.GetComponent<SpriteRenderer>().enabled = true;

                flag = true;
            }
        }
        
        
    }


    public void Boss_HP_Update()
    {
        Hp_UI.value = Hp;
    }

    public void Damage()
    {
        Hp -= 1;
    }

    public void Die()
    {
        isDie = true;
        while(bullets.Count != 0)
        {
            Destroy(bullets.Dequeue().gameObject);
        }

        if (!clear_flag)
        {
            GameSceneManager.Instance.bossEnd();
            clear_flag = true;
        }
    }
}
