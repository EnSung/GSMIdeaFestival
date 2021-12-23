using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (!GameSceneManager.Instance.isTeleport)
        {
            runAwayCheck();
        }
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
        bool teleport_distance_flag = false;

        int cnt = 0;
        while (state == enemyState.following) // 따라가는 상태일때
        {
            yield return null;
            if (!teleport_distance_flag) // 텔레포트 상태가 아니면
            {

                dis = Vector2.Distance(transform.position, player.transform.position); // 거리 갱신
            }

            if (GameSceneManager.Instance.isTeleport) // 텔레포트했다면
            {

                teleport_distance_flag = true; // 텔레보트 플래그 활성화

                if (dis > 20) // 거리가 너무멀면
                {
                    dis = 4; // 단축
                }

                float time = Time.time + (dis / 2); // 현재 시간에 거리의 1/2 더하기

                while (time > Time.time) // dis/2 만큼 대기하기
                {
                    yield return null; // 한프레임쉬기


                    if (GameSceneManager.Instance.playerTeleportObject == GameSceneManager.Instance.prev_playerTeleportObject)  // 기다리는동안 플레이어가 다시 나오지않았다면
                    {
                        isOut = true; // 나갔는지 체크하는 플래그 true
                        GameSceneManager.Instance.prev_playerTeleportObject = null; // 비교를 위한 오브젝트 삭제
                        break;// while문 나가기
                    }

                }

                GameSceneManager.Instance.prev_playerTeleportObject = null; // 비교를 위한 오브젝트 삭제


                if (!isOut) // 플래그가 활성화 되지않았다면
                {
                    this.transform.position = GameSceneManager.Instance.playerTeleportObject.transform.position; // 위치를 플레이어가 이동했던 물체로 이동.

                }

                // 텔레포트가 끝났으니
                GameSceneManager.Instance.isTeleport = false; // 텔레포트가 끝났으니 플래그 비활성화
                Debug.Log(14);
                teleport_distance_flag = false; // distance플래그 비활성화
                Debug.Log(15);
                isOut = false; // 나갔는지 체크하는 플래그 비활성화
            }
            else
            {
                if (isFollowingCancel)
                {
                    float time = Time.time + 0.1f;

                    if (cnt == 0)
                    {
                        while (time > Time.time)
                        {
                            yield return null;
                            if (GameSceneManager.Instance.isTeleport)
                            {
                                isFollowingCancel = false;
                                collider.enabled = true;
                                cnt++;
                                break;
                            }
                        }
                    }
                    else
                    {
                        while (Vector2.Distance(transform.position, originPos) >= 0.1f)
                        {
                            yield return null;
                            collider.enabled = false;


                            transform.position = Vector2.MoveTowards(transform.position, originPos, speed * Time.deltaTime);
                            lookat2D(originPos);
                        }
                    }



                }
                else
                {
                    collider.enabled = true;
                    lookat2D(player.transform.position);
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followingSpeed * Time.deltaTime);
                }

            }

            

            cnt = 0;
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

    /// <summary>
    /// 안씀
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
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
            if (!GameSceneManager.Instance.isTeleport)
            {
                isFollowingCancel = true;

            }
        }
        else
        {
            isFollowingCancel = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, runAwayCheckRadius);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}




