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

    public float runAwayCheckRadius;
    #region Flag
    bool following_flag = false;
    bool patrol_flag = false;

    public bool isFollowingCancel = false;

    #endregion

    #region stat
    public enemyState state;
    public float speed;
    public float followingSpeed;
    #endregion

    GameObject player;

    public Vector2 originPos;
    public Vector2 targetPos;

    #region Warn Image
    public WarnSprite patrolWarnSp;
    public WarnSprite normalWarnSp;
    public GameObject followingWarnSp;


    public WarnSprite curWarnSp;
    public GameObject bangImg;
    #endregion

    public EnemyFieldOfView fieldOfView;
    public Collider2D collider;

    private void Start()
    {
        fieldOfView = GetComponent<EnemyFieldOfView>();
        curWarnSp = patrolWarnSp;

        fieldOfView.m_horizontalViewAngle = curWarnSp.angle;
        fieldOfView.m_viewRadius = curWarnSp.distance;

        collider = GetComponent<Collider2D>();


        player = GameObject.FindGameObjectWithTag("Player");
        originPos = transform.position;




        state = enemyState.patrol;

        patrol();

    }
    private void Update()
    {


        if (state == enemyState.patrol)
        {
            patrol();
        }
        else if (state == enemyState.following)
        {

            following();
        }

    }

    private void FixedUpdate()
    {
        runAwayCheck();
    }



    #region beginFunc
    public void patrol()
    {
        if (!patrol_flag)
        {
            StartCoroutine(patrolCroutine());

        }
    }

    public void following()
    {
        if (!following_flag)
        {
            StartCoroutine(followingCroutine());
        }
    }
    #endregion

    #region Croutine
    IEnumerator patrolCroutine()
    {

        patrol_flag = true;
        curWarnSp.gameObject.SetActive(true);
        normalWarnSp.gameObject.SetActive(true);

        lookat2D(targetPos);

        while (state == enemyState.patrol)
        {

            while (Vector2.Distance((Vector2)transform.position, targetPos) >= 0.1f)
            {
                GameSceneManager.Instance.isTeleport = false;
                if (state != enemyState.patrol) break;


                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                yield return null;

            }

            GameSceneManager.Instance.isTeleport = false;

            if (state != enemyState.patrol) break;

            yield return new WaitForSeconds(1f);


            lookat2D(originPos);

            yield return new WaitForSeconds(1f);


            while (Vector2.Distance((Vector2)transform.position, originPos) >= 0.1f)
            {
                GameSceneManager.Instance.isTeleport = false;

                if (state != enemyState.patrol) break;


                transform.position = Vector2.MoveTowards(transform.position, originPos, speed * Time.deltaTime);
                yield return null;

            }

            if (state != enemyState.patrol) break;

            yield return new WaitForSeconds(1f);

            lookat2D(targetPos);


            yield return new WaitForSeconds(1f);


        }

        patrol_flag = false;
    }


    IEnumerator followingCroutine()
    {
        following_flag = true;

        yield return null;
        GameSceneManager.Instance.isTeleport = false;

        bangImg.SetActive(true);
        curWarnSp.gameObject.SetActive(false);
        normalWarnSp.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.4f);

        bangImg.SetActive(false);
        followingWarnSp.SetActive(true);

        float dis = 0;
        bool isOut = false;
        bool temp = false;

        int cnt = 0;
        while (state == enemyState.following)
        {

            if (!temp)
            {
                dis = Vector2.Distance(transform.position, player.transform.position);
            }

            if (GameSceneManager.Instance.isTeleport)
            {
                temp = true;

                if (dis > 20)
                {
                    dis = 4;
                }

                float time = Time.time + (dis / 2);

                while (time > Time.time)
                {
                    yield return null;
                                
                    if (GameSceneManager.Instance.playerTeleportObject == GameSceneManager.Instance.prev_playerTeleportObject)
                    {
                        isOut = true;
                        GameSceneManager.Instance.prev_playerTeleportObject = null;
                        break;
                    }


                }

                GameSceneManager.Instance.prev_playerTeleportObject = null;

                if (!isOut)
                {
                    transform.position = GameSceneManager.Instance.playerTeleportObject.transform.position;
                }


                temp = false;
                isOut = false;
                GameSceneManager.Instance.isTeleport = false;
            }
            else
            {


                if (isFollowingCancel)
                {
                    float time = Time.time + 0.5f;

                    if (cnt != 0)
                    {
                        while (time > Time.time)
                        {
                            yield return null;
                            if (!isFollowingCancel)
                            {
                                collider.enabled = true;
                                break;
                            }
                            else
                            {
                                collider.enabled = false;
                                cnt++;

                            }

                        }
                    }


                    transform.position = Vector2.MoveTowards(transform.position, originPos, speed * Time.deltaTime);

                    lookat2D(originPos);
                    if (Vector2.Distance(transform.position, originPos) <= 0.1f)
                    {
                        collider.enabled = true;

                        state = enemyState.patrol;
                        break;
                    }
                }
                else
                {
                    collider.enabled = true;
                    lookat2D(player.transform.position);
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followingSpeed * Time.deltaTime);

                }

            }

            yield return null;

        }

        following_flag = false;
        followingWarnSp.SetActive(false);
    }

    #endregion

    #region Lookat
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

        if (transform.rotation == Quaternion.AngleAxis(angle - 90f, Vector3.forward))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    #endregion


    void runAwayCheck()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, runAwayCheckRadius, LayerMask.GetMask("Player"));

        if (hit == null)
        {
            isFollowingCancel = true;
        }
        else
        {
            isFollowingCancel = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, runAwayCheckRadius);
    }
}




