using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomWall : Bonus
{
    protected override void AddEffect()
    {
        m_player.SetWallActive(true);
    }

}
