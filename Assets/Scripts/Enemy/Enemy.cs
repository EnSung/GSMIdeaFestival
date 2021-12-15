using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public enum enemyState
    {
        patrol = 10,
        moving,
        following,
    }

    bool isfollowingstart = false;

    public enemyState state;

    public float speed;
    public float followingSpeed;
    GameObject player;

    public Vector2 originPos;
    public Vector2 targetPos;

    public WarnSprite patrolWarnSp;
    public WarnSprite normalWarnSp;

    public WarnSprite curWarnSp;

    public EnemyFieldOfView fieldOfView;
    private void Start()
    {
        fieldOfView = GetComponent<EnemyFieldOfView>();
        curWarnSp = patrolWarnSp;

        fieldOfView.m_horizontalViewAngle = curWarnSp.angle;
        fieldOfView.m_viewRadius = curWarnSp.distance;




        player = GameObject.FindGameObjectWithTag("Player");
        originPos = transform.position; 

        
        

        state = enemyState.patrol;

        patrol();

    }
    private void Update()
    {



        if (state == enemyState.following)
        {

            following();
        }

    }


    public void patrol()
    {
        StartCoroutine(patrolCroutine());
    }

    public void following()
    {
        if (!isfollowingstart)
        {
            StartCoroutine(followingCroutine());
        }
    }

    IEnumerator patrolCroutine()
    {

        lookat2D(targetPos);

        while (state == enemyState.patrol)
        {

            while (Vector2.Distance((Vector2)transform.position,targetPos) >= 0.1f)
            {
                if (state != enemyState.patrol) break;


                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime) ;
                yield return null;

            }

            if (state != enemyState.patrol) break;

            yield return new WaitForSeconds(1f);


            lookat2D(originPos);

            yield return new WaitForSeconds(1f);


            while (Vector2.Distance((Vector2)transform.position,originPos) >= 0.1f)
            {
                if (state != enemyState.patrol) break;


                transform.position = Vector2.MoveTowards(transform.position, originPos, speed * Time.deltaTime);
                yield return null;

            }

            if (state != enemyState.patrol) break;

            yield return new WaitForSeconds(1f);

            lookat2D(targetPos);
            
                
            yield return new WaitForSeconds(1f);


        }

    }


    IEnumerator followingCroutine()
    {
        isfollowingstart = true;
        yield return null;

        curWarnSp.gameObject.SetActive(false);
        curWarnSp.gameObject.SetActive(false);

        float dis = 0;
        bool isout = false;
        bool boolean = false;
        while (state == enemyState.following) {

            if (!boolean)
            {
                dis = Vector2.Distance(transform.position, player.transform.position);
            }

            if (GameSceneManager.Instance.isTeleport)
            {
                boolean = true;

                if(dis > 20)
                {
                    dis = 10;
                }
                
                float time = Time.time + (dis/2);
                
                while(time > Time.time)
                {
                    yield return null;
                    if(Vector2.Distance(transform.position, player.transform.position) < 40)
                    {
                        isout = true;
                        break;
                        
                    }
                }
                if(!isout)  transform.position = GameSceneManager.Instance.playerTeleportObject.transform.position;

                isout = false;
                GameSceneManager.Instance.isTeleport = false;
            }
            else
            {
                lookat2D(player.transform.position);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followingSpeed * Time.deltaTime);

            }

            yield return null;

        }

        isfollowingstart = false;
    }

    

    public void lookat2D(Vector3 target)
    {

        Vector3 dir = (target - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    public bool lookat2DSlerp(Vector3 target)
    {

        Vector3 direction = (target - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 10 * Time.deltaTime);
        transform.rotation = rotation;

        if(transform.rotation ==  Quaternion.AngleAxis(angle - 90f, Vector3.forward))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}




