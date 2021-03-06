using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    [SerializeField]int enemyhitpoint = 50;
    [SerializeField] GameObject m_effectPrefab = default;
    [SerializeField] GameObject m_clonePrefab = default;
    [SerializeField] GameObject m_eateffect = default;

    [SerializeField] AudioClip m_audio = default;
    [SerializeField] GameObject m_DropItem = default;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shell")
        {
            Debug.Log("hit");
            enemyhitpoint =enemyhitpoint- 10;
            // エフェクトとなるプレハブが設定されていたら、それを生成する
            if (m_effectPrefab)
            {
                Instantiate(m_effectPrefab, this.transform.position, this.transform.rotation);
            }
            
        }

        if (collision.gameObject.tag == "Niku")
        {
            Debug.Log("eat");
            enemyhitpoint = enemyhitpoint + 30;
            Instantiate(m_eateffect, this.transform.position, this.transform.rotation);
            AudioSource.PlayClipAtPoint(m_audio, Camera.main.transform.position);


        }

            

        
        if (enemyhitpoint <= 0)
        {
            Instantiate(m_DropItem, this.transform.position, this.transform.rotation);
            // 自分自身を破棄する
            Destroy(this.gameObject);
        }
    }
}
