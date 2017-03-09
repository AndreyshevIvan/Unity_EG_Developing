using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Bonus
{
    protected override void AddEffect()
    {
        m_player.SetFireBall(true);
    }

}
