using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultyBall : Bonus
{

    protected override void AddEffect()
    {
        m_player.AddLife();
    }
}
