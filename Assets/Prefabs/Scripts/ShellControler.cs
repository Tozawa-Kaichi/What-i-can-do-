using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellControler : MonoBehaviour
{
    /// <summary>砲弾が当たった時に表示されるエフェクト</summary>
    [SerializeField] GameObject m_effectPrefab = default;
    /// <summary>発射する速度</summary>
    [SerializeField] float m_initialSpeed = 5f;
    AudioSource m_audio = default;
    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody を取得して発射する
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = this.transform.up * m_initialSpeed;
        m_audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -10f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            // エフェクトとなるプレハブが設定されていたら、それを生成する
            if (m_effectPrefab)
            {
                Instantiate(m_effectPrefab, this.transform.position, this.transform.rotation);
            }

            // 自分自身を破棄する
            Destroy(this.gameObject);
        }
        // 何かにぶつかったら自分自身を破棄する
        Destroy(this.gameObject);
    }
}
