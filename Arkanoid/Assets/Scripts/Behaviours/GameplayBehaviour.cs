using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBehaviour : GameBehaviour
{
    bool m_isPause = false;

    public override void StartOptions()
    {
        m_cameraController.ResetOptions();
        m_ballController.Reset();
        m_platformController.Reset();
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

        if (m_isPause)
        {
            m_ballController.StopBall();
        }
        else
        {
            m_ballController.StartBall();
        }
    }

    void PauseUpdate()
    {

    }

    void GameplayUpdate()
    {
        m_cameraController.HandleCameraEvents();
        m_platformController.HandlePlatfomEvents();
        m_ballController.HandleBallEvents();
    }
}