using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NikuController : MonoBehaviour
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


    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move();                     // 例題
        MoveWithTurn();          // 課題4
        //MoveOnFloorWithStop();   // 例題
        //MoveOnFloorWithTurn();   // 課題5
    }

    /// <summary>
    /// 例題: 前方に壁を検出するまで右に移動し、壁を検出したら停止する処理を書け。
    /// </summary>
    void Move()
    {
        Vector2 origin = this.transform.position;   // origin は「raycast の始点」である
        Debug.DrawLine(origin, origin + m_rayForWall);  // ray（光線）を Scene 上に描く
        // Raycast して壁の検出を試みる
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, m_rayForWall, m_rayForWall.magnitude, m_wallLayer);   // hit には ray の衝突情報が入っている
        Vector2 dir = Vector2.zero; // dir は速度ベクトル

        if (!hit.collider)  // hit.collider は「ray が衝突した collider」が入っている。ray が何にもぶつからなかったら null である。
        {
            // 何も検出しなかったら速度ベクトルを計算する
            dir = Vector2.right * m_moveSpeed;
        }

        dir.y = m_rb.velocity.y;    // 落下については現在の値を採用する
        m_rb.velocity = dir;    // 速度ベクトルをセットする
    }

    /// <summary>
    /// 課題4:
    /// 前方に壁を検出するまで右に移動し、壁を検出したら左に移動する処理を書け。
    /// 左に移動している時にも前方に壁を検出したら方向転換して右に移動させよ。
    /// </summary>
    void MoveWithTurn()
    {
        Vector2 lscale = gameObject.transform.localScale;
        Vector2 origin = this.transform.position;
        Vector2 dir = Vector2.zero;
        if (wallsearch == false)
        {
            Debug.DrawLine(origin, origin + m_rayForWall);
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, m_rayForWall, m_rayForWall.magnitude, m_wallLayer);
            if (!hit.collider && !wallsearch)
            {
                dir = Vector2.right * m_moveSpeed;
                if (lscale.x < 0)
                {
                    lscale.x *= -1;
                }
                gameObject.transform.localScale = lscale;
            }
            else if (hit.collider)
            {
                wallsearch = true;
            }
           
        }
        if (wallsearch)
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, -1 * m_rayForWall, m_rayForWall.magnitude, m_wallLayer);
            Debug.DrawLine(origin, origin - m_rayForWall);
            if (!hit.collider && wallsearch)
            {
                dir = Vector2.left * m_moveSpeed;
                if (lscale.x > 0)
                {
                    lscale.x *= -1;
                }
                gameObject.transform.localScale = lscale;
            }
            else if (hit.collider)
            {
                wallsearch = false;
            }
        }
        dir.y = m_rb.velocity.y;    // 落下については現在の値を採用する
        m_rb.velocity = dir;
    }

    /// <summary>
    /// 例題: 床を検出している間は右移動し、床がないことを検出したら停止する処理を書け。
    /// </summary>
    void MoveOnFloorWithStop()
    {
        Vector2 origin = this.transform.position;
        Debug.DrawLine(origin, origin + m_rayForGround);    // ray を Scene 上に描く
        // Raycast して床の検出を試みる
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, m_rayForGround, m_rayForGround.magnitude, m_groundLayer);
        Vector2 dir = Vector2.zero; // dir は速度ベクトル

        if (hit.collider)
        {
            // 床を検出したら速度ベクトルを計算する
            dir = Vector2.right * m_moveSpeed;
        }
        dir.y = m_rb.velocity.y;    // 落下については現在の値を採用する
        m_rb.velocity = dir;    // 速度ベクトルをセットする
    }

    /// <summary>
    /// 課題5:
    /// 前方に床を検出している間は右移動し、床が検出できない時は方向転換して左に移動する処理を書け。
    /// 左に移動している時にも前方に床がないことを検出したら方向転換して右に移動させよ。
    /// </summary>
    void MoveOnFloorWithTurn()
    {
        Vector2 origin = this.transform.position;
        Vector2 dir = Vector2.zero;
        if (!groundsearch)
        {
            Debug.DrawLine(origin, origin + m_rayForGround);
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, m_rayForGround, m_rayForGround.magnitude, m_groundLayer);
            if (hit.collider && !groundsearch)
            {
                dir = Vector2.right * m_moveSpeed;
            }
            else if (!hit.collider)
            {
                groundsearch = true;
            }
        }
        else if (groundsearch)
        {
            Debug.DrawLine(origin, origin + m_rayForGroundreturn);
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, m_rayForGroundreturn, m_rayForGroundreturn.magnitude, m_groundLayer);

            if (hit.collider && groundsearch)
            {
                dir = Vector2.left * m_moveSpeed;
            }
            if (!hit.collider)
            {
                groundsearch = false;
            }
        }
        dir.y = m_rb.velocity.y;    // 落下については現在の値を採用する
        m_rb.velocity = dir;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // 自分自身を破棄する
            Destroy(this.gameObject);

        }
    }
}
