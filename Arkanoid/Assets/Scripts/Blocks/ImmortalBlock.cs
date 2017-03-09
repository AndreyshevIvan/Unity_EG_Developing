using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalBlock : Block
{

    protected override void PersonalAwake()
    {
        m_isImmortal = true;
    }
}
