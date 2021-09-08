using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunmanController : CharacterBase2D
{
    /// <summary>左右移動する力</summary>
    [SerializeField] public float m_movePower = 5f;
    /// <summary>ジャンプする力</summary>
    [SerializeField] float m_jumpPower = 15f;
    // Start is called before the first frame update
    /// <summary>入力に応じて左右を反転させるかどうかのフラグ</summary>
    [SerializeField] bool m_flipX = false;
    /// <summary>連続で（接地せずに）ジャンプ可能な回数</summary>
    [SerializeField] int m_maxJumps = 2;
    Rigidbody2D m_rb = default;
    SpriteRenderer m_sprite = default;
    /// <summary>m_colors に使う添字</summary>
    int m_colorIndex;
    /// <summary>水平方向の入力値</summary>
    float m_h;
    float m_scaleX;
    /// <summary>最初に出現した座標</summary>
    Vector3 m_initialPosition;
    /// <summary>接地判定変数</summary>
    bool m_isGrounded = false;
    /// <summary>ジャンプした回数のカウンタ</summary>
    int m_jumpCount;

    List<ItemBase2D> m_itemList = new List<ItemBase2D>();
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        // 初期位置を覚えておく
        m_initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 入力を受け取る
        m_h = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("ここにジャンプする処理を書く。");

            if (m_isGrounded)
            {
                this.m_rb.AddForce(Vector2.up * this.m_jumpPower, ForceMode2D.Impulse);
            }
            else if (m_jumpCount < m_maxJumps)
            {
                this.m_rb.AddForce(Vector2.up * this.m_jumpPower, ForceMode2D.Impulse);
            }

            m_jumpCount++;
        }
        // 下に行きすぎたら初期位置に戻す
        if (this.transform.position.y < -10f)
        {
            this.transform.position = m_initialPosition;
        }
        // 設定に応じて左右を反転させる
        if (m_flipX)
        {
            FlipX(m_h);
        }

    }
    private void FixedUpdate()
    {
        // 力を加えるのは FixedUpdate で行う
        m_rb.AddForce(Vector2.right * m_h * m_movePower, ForceMode2D.Force);
    }
    public void GetItem(ItemBase2D item)
    {
        m_itemList.Add(item);
    }
    void FlipX(float horizontal)
    {
        /*
         * 左を入力されたらキャラクターを左に向ける。
         * 左右を反転させるには、Transform:Scale:X に -1 を掛ける。
         * Sprite Renderer の Flip:X を操作しても反転する。
         * */
        m_scaleX = this.transform.localScale.x;
        if (horizontal > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (horizontal < 0)
        {
            this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 足下にトリガーを追加しておくこと。足下のトリガーに地面が接触したら「接地している」とみなす。
        m_isGrounded = true;
        m_jumpCount = 0;
        if (collision.gameObject.tag == "Enemy")
        {
            // 自分自身を破棄する
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 足下にトリガーを追加しておくこと。足下のトリガーから地面が離れたら「接地していない」とみなす。
        m_isGrounded = false;
    }
    
}
