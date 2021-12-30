using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterInRoom : MonoBehaviour
{
    [SerializeField] float reTime;

    public bool isFind; // 안쪽감시경우 발견 여부

    bool find_flag;
    float find_timer;
    public enum master_type
    {
        observe_inside,
        observe_outside
    }

    public master_type master_Type;
    public bool isObserve;

    [SerializeField] GameObject sight;
    [SerializeField] GameObject questionMark;
    [SerializeField] GameObject bangMark;
    PlayerController player;

    public SpriteRenderer window;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        switch (master_Type)
        {
            case master_type.observe_inside:
                StartCoroutine(observe_Inside());
                break;
            case master_type.observe_outside:
                StartCoroutine(Observe_Outside());
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        
        if (isFind && !find_flag)
        {
            StartCoroutine(find());
        }
    }

    IEnumerator Observe_Outside()
    {
        yield return null;
        while (true)
        {
            yield return null;

            yield return new WaitForSeconds(reTime);

            isObserve = true;
            sight.SetActive(true);
            window.color = new Color(0, 104, 255);

            yield return new WaitForSeconds(reTime);

            window.color = new Color(0, 0,0);

            isObserve = false;
            sight.SetActive(false);
            player.canMove_master = true;
        }
    }


    IEnumerator observe_Inside()
    {

        yield return new WaitForSeconds(2);
        yield return null;
        while (true)
        {
            if (isFind) break;

            yield return null;

            reTime = Random.Range(2, 5);

            float time = Time.time + reTime;

            while(time >= Time.time)
            {
                yield return null;
                if (isFind) break;
            }
            if (isFind) break;

            questionMark.SetActive(true);

            yield return new WaitForSeconds(0.57f);

            questionMark.SetActive(false);
            
            isObserve = true;
            
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

            reTime = Random.Range(1, 3);

            time = Time.time + reTime;
            while (time >= Time.time)
            {
                yield return null;

                if (isFind) break;
            }

            if (isFind) break;

            isObserve = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));

        }
    }

    IEnumerator find()
    {
        find_flag = true;

        bangMark.SetActive(true);
        yield return new WaitForSeconds(0.6f);

            SceneManager.LoadScene("GameOverScene");

        find_flag = false;
    }
}
