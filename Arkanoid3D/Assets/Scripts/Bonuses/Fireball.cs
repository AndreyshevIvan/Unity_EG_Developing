using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Bonus
{
    protected override void AddEffect()
    {
        m_player.SetFireBallsMode(true);
    }
}
