using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverBehaviour : GameBehaviour
{

    public override void StartOptions()
    {
        m_blocksController.ClearBlocks();
    }

    public override void UpdateBehavior()
    {

    }
}