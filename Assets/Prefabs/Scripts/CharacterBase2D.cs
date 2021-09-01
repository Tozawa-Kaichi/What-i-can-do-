using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase2D : MonoBehaviour
{
    [SerializeField] AudioClip m_sound = default;
    int hitpoint=0;
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
}
