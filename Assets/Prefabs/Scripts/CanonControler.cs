using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonControler : MonoBehaviour
{
    /// <summary>砲弾として生成するプレハブ</summary>
    [SerializeField] GameObject m_shellPrefab = default;
    /// <summary>砲口を指定する</summary>
    [SerializeField] Transform m_muzzle = default;
    [SerializeField] Transform Crosshair = default;
    AudioSource m_audio = default;
    [SerializeField] float m_interval = 1f;
    float m_timer;
    bool m_inter = true;
    // Start is called before the first frame update
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && m_inter == true)
        {
            m_inter = false;
            m_timer = 0;
            m_audio.Play();
            Instantiate(m_shellPrefab, m_muzzle.position, this.transform.rotation);

        }
        Vector2 a = Crosshair.transform.position - this.transform.position;
        m_timer += Time.deltaTime;
        if (m_timer > m_interval)
        {
            m_timer = 0;    // タイマーをリセットしている
            m_inter = true;
        }
        this.transform.up = a;
    }
}
