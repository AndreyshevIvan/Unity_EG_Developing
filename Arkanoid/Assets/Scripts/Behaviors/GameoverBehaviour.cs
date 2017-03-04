using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverBehaviour : GameBehaviour
{

    public override void UpdateBehavior()
    {
        Debug.Log("GameoverBehaviour");

        m_items.time += Time.deltaTime;
        if (m_items.time >= 3)
        {
            m_items.time = 0;
            m_controller.SwitchBehaviour();
        }
    }
}