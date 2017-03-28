using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMode : Bonus
{
    protected override void AddEffect()
    {
        m_player.SetAttackMode(true);
    }
}
