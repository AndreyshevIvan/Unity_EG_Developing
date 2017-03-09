using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Bonus
{
    protected override void AddEffect()
    {
        m_player.AddLife();
    }

}
