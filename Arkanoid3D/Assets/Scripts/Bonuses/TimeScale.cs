using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : Bonus
{
    protected override void AddEffect()
    {
        m_player.SetTimeScale(true);
    }
}
