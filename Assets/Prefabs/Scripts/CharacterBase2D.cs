using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase2D : MonoBehaviour
{
    [SerializeField] AudioClip m_sound = default;
    int hitpoint=0;
    /// <summary>最大ライフ</summary>
    [SerializeField] int m_maxLife = 100;
    /// <summary>初期ライフ</summary>
    [SerializeField] int m_initialLife = 10;
    int m_score = 0;
    int m_life = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitpoint <= 0)
        {
            // 自分自身を破棄する
            Destroy(this.gameObject);
        }
    }

    public void AddLife(int life)
    {
        m_life =m_life+ life;
    }
}
