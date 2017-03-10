using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMultiplitter : Bonus
{

    protected override void AddEffect()
    {
        m_player.AddMultiplitter();
    }
}
