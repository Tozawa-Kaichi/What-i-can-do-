using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDrink : ItemBase2D
{
    /// <summary>ライフを回復（減少）させる値</summary>
    [SerializeField] int m_recoverLife = 10;
    

    
    public override void Activate()
    {
        FindObjectOfType<CharacterBase2D>().AddLife(m_recoverLife);
    }
}
