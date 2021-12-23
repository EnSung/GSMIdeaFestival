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
        while (state == enemyState.following) // ���󰡴� �����϶�
        {
            yield return null;
            if (!teleport_distance_flag) // �ڷ���Ʈ ���°� �ƴϸ�
            {

                dis = Vector2.Distance(transform.position, player.transform.position); // �Ÿ� ����
            }

            if (GameSceneManager.Instance.isTeleport) // �ڷ���Ʈ�ߴٸ�
            {

                teleport_distance_flag = true; // �ڷ���Ʈ �÷��� Ȱ��ȭ

                if (dis > 20) // �Ÿ��� �ʹ��ָ�
                {
                    dis = 4; // ����
                }

                float time = Time.time + (dis / 2); // ���� �ð��� �Ÿ��� 1/2 ���ϱ�

                while (time > Time.time) // dis/2 ��ŭ ����ϱ�
                {
                    yield return null; // �������ӽ���


                    if (GameSceneManager.Instance.playerTeleportObject == GameSceneManager.Instance.prev_playerTeleportObject)  // ��ٸ��µ��� �÷��̾ �ٽ� �������ʾҴٸ�
                    {
                        isOut = true; // �������� üũ�ϴ� �÷��� true
                        GameSceneManager.Instance.prev_playerTeleportObject = null; // �񱳸� ���� ������Ʈ ����
                        break;// while�� ������
                    }

                }

                GameSceneManager.Instance.prev_playerTeleportObject = null; // �񱳸� ���� ������Ʈ ����


                if (!isOut) // �÷��װ� Ȱ��ȭ �����ʾҴٸ�
                {
                    this.transform.position = GameSceneManager.Instance.playerTeleportObject.transform.position; // ��ġ�� �÷��̾ �̵��ߴ� ��ü�� �̵�.

                }

                // �ڷ���Ʈ�� ��������
                GameSceneManager.Instance.isTeleport = false; // �ڷ���Ʈ�� �������� �÷��� ��Ȱ��ȭ
                Debug.Log(14);
                teleport_distance_flag = false; // distance�÷��� ��Ȱ��ȭ
                Debug.Log(15);
                isOut = false; // �������� üũ�ϴ� �÷��� ��Ȱ��ȭ
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
    /// �Ⱦ�
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




