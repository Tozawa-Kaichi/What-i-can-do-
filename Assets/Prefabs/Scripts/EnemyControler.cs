using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    [SerializeField]int enemyhitpoint = 50;
    [SerializeField] GameObject m_effectPrefab = default;
    [SerializeField] GameObject m_clonePrefab = default;
    AudioSource m_audio = default;
    
    // Start is called before the first frame update
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        
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
      
            Debug.Log("eat");
            enemyhitpoint = enemyhitpoint + 30;
            
            
            Instantiate(m_clonePrefab, this.transform.parent);

            

        
        if (enemyhitpoint <= 0)
        {
            // 自分自身を破棄する
            Destroy(this.gameObject);
        }
    }
}
