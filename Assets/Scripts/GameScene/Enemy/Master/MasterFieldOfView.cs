using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterFieldOfView : MonoBehaviour
{

    [SerializeField] bool isNormalSight;
    [SerializeField] private bool m_bDebugMode = false;

    [Header("View Config")]
    [Range(0f, 360f)]
    [SerializeField] public float m_horizontalViewAngle = 0f; // �þ߰�
    [SerializeField] public float m_viewRadius = 1f; // �þ� ����
    [Range(-180f, 180f)]
    [SerializeField] private float m_viewRotateZ = 0f; // �þ߰��� ȸ����

    [SerializeField] private LayerMask m_viewTargetMask;       // �ν� ������ Ÿ���� ����ũ
    [SerializeField] private LayerMask m_viewObstacleMask;     // �ν� ���ع��� ����ũ 

    private List<Collider2D> hitedTargetContainer = new List<Collider2D>(); // �ν��� ��ü���� ������ �����̳�

    private float m_horizontalViewHalfAngle = 0f; // �þ߰��� ���� ��

    MasterInRoom master;
    [SerializeField] PlayerController player;

    private void Awake()
    {
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;
        master = GetComponent<MasterInRoom>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    private void OnDrawGizmos()
    {
        if (m_bDebugMode)
        {
            m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;

            Vector3 originPos = transform.position;

            Gizmos.DrawWireSphere(originPos, m_viewRadius);

            Vector3 horizontalRightDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 horizontalLeftDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 lookDir = AngleToDirZ(m_viewRotateZ);

            Debug.DrawRay(originPos, horizontalLeftDir * m_viewRadius, Color.cyan);
            Debug.DrawRay(originPos, lookDir * m_viewRadius, Color.green);
            Debug.DrawRay(originPos, horizontalRightDir * m_viewRadius, Color.cyan);

        }
    }

    private void FixedUpdate()
    {
        FindViewTargets();
    }

    public Collider2D[] FindViewTargets()
    {

        if (master.isObserve)
        {
            hitedTargetContainer.Clear();

            Vector2 originPos = transform.position;
            Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, m_viewRadius, m_viewTargetMask);

            foreach (Collider2D hitedTarget in hitedTargets)
            {
                Vector2 targetPos = hitedTarget.transform.position;
                Vector2 dir = (targetPos - originPos).normalized;
                Vector2 lookDir = AngleToDirZ(m_viewRotateZ);

                // float angle = Vector3.Angle(lookDir, dir)
                // �Ʒ� �� ���� ���� �ڵ�� �����ϰ� ������. ���� ������ ����
                float dot = Vector2.Dot(lookDir, dir);
                float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

                if (angle <= m_horizontalViewHalfAngle)
                {
                    RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, m_viewRadius, m_viewTargetMask);
                    if (rayHitedTarget)
                    {


                        if (m_bDebugMode)
                        {
                            Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
                        }


                        player.canMove_master = false;
                    }
                    else
                    {
                        hitedTargetContainer.Add(hitedTarget);

                        if (m_bDebugMode)
                            Debug.DrawLine(originPos, targetPos, Color.red);

                        // ������ ������

                        Debug.Log(rayHitedTarget.collider.name);
                        player.canMove_master = true;    
                    }
                }
            }

            if (hitedTargetContainer.Count > 0)
                return hitedTargetContainer.ToArray();
            else
                return null;
        }
        else
        {
            return null;

        }

    }

    // -180~180�� ���� Up Vector ���� Local Direction���� ��ȯ������.
    private Vector2 AngleToDirZ(float angleInDegree)
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian));
    }
}
