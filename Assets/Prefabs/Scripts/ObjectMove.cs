using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectMove : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] float m_moveSpeed = 1f;
    /// <summary>壁を検出するための ray のベクトル</summary>
    [SerializeField] Vector2 m_rayForWall = Vector2.zero;
    /// <summary>壁のレイヤー（レイヤーはオブジェクトに設定されている）</summary>
    [SerializeField] LayerMask m_wallLayer = 0;
    /// <summary>床を検出するための ray のベクトル</summary>
    [SerializeField] Vector2 m_rayForGround = Vector2.zero;
    [SerializeField] Vector2 m_rayForGroundreturn = Vector2.zero;
    /// <summary>床のレイヤー</summary>
    [SerializeField] LayerMask m_groundLayer = 0;
    Rigidbody2D m_rb = default;
    bool wallsearch = false;
    bool groundsearch = false;
    Vector2 migi = Vector2.right;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveOnFloor();
    }
    void MoveOnFloor()
    {
        Vector2 origin = this.transform.position;
        Vector2 dir = Vector2.zero;

        if (!groundsearch && !wallsearch)
        {
            Debug.DrawLine(origin, origin + m_rayForWall);
            Debug.DrawLine(origin, origin + m_rayForGround);
            RaycastHit2D hit1 = Physics2D.Raycast(this.transform.position, m_rayForWall, m_rayForWall.magnitude, m_wallLayer);
            RaycastHit2D hit2 = Physics2D.Raycast(this.transform.position, m_rayForGround, m_rayForGround.magnitude, m_groundLayer);
            if (!hit1.collider && hit2 && !wallsearch && !groundsearch)
            {
                dir = Vector2.right * m_moveSpeed;
            }
            else if (hit1.collider || !hit2.collider)
            {
                wallsearch = true;
                groundsearch = true;
            }
        }
        else if (groundsearch && wallsearch)
        {
            Debug.DrawLine(origin, origin - m_rayForWall);
            Debug.DrawLine(origin, origin + m_rayForGroundreturn);
            RaycastHit2D hit1 = Physics2D.Raycast(this.transform.position, -1 * m_rayForWall, m_rayForWall.magnitude, m_wallLayer);
            RaycastHit2D hit2 = Physics2D.Raycast(this.transform.position, m_rayForGroundreturn, m_rayForGroundreturn.magnitude, m_groundLayer);
            if (!hit1.collider && hit2 && wallsearch && groundsearch)
            {
                dir = Vector2.left * m_moveSpeed;
            }
            else if (hit1.collider || !hit2.collider)
            {
                wallsearch = false;
                groundsearch = false;
            }
        }
        dir.y = m_rb.velocity.y;
        m_rb.velocity = dir;

    }
}
