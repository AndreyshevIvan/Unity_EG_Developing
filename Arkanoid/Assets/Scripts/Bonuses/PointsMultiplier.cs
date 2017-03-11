using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMultiplier : Bonus
{

    protected override void AddEffect()
    {
        m_player.AddMultiplier();
    }
}
