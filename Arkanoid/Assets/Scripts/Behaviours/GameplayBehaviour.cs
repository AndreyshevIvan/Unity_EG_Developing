using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBehaviour : GameBehaviour
{
    bool m_isPause = false;

    public override void StartOptions()
    {
        m_cameraController.ResetOptions();
        m_platformController.Reset();
        m_ballsController.Reset();
        m_blocksController.ClearBlocks();
        m_spawner.SpawnLevel();
    }

    public override void UpdateBehavior()
    {
        if (m_isPause)
        {
            PauseUpdate();
        }
        else
        {
            GameplayUpdate();
        }
    }

    public void SetPause(bool isPause)
    {
        m_isPause = isPause;
        m_ballsController.FreezeAll(isPause);
    }

    void PauseUpdate()
    {

    }
    void GameplayUpdate()
    {
        m_cameraController.HandleEventsAndUpdate();
        m_platformController.HandleEventsAndUpdate();
        m_ballsController.HandleEventsAndUpdate();
    }
}