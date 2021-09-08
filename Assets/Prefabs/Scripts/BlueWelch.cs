using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWelch : ItemBase2D
{
    [SerializeField] float speedup = 10;
    public override void Activate()
    {
        FindObjectOfType<GunmanController>().m_movePower+=speedup;

    }
}

