using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{

    public GameObject m_pauseItems;
    public GameObject m_gameplayItems;

    bool m_isPause = false;

    public Platform m_platform;
    public BallsController m_ballsController;
    public BlocksController m_blocksController;

    private void Awake()
    {
        StartLevel();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SetPause(!m_isPause);
        }

        if (m_isPause)
        {
            PauseUpdate();
        }
        else
        {
            GameUpdate();
        }
    }
    void PauseUpdate()
    {

    }
    void GameUpdate()
    {
        m_platform.HandleEventsAndUpdate();
    }

    public void SetPause(bool isPause)
    {
        m_isPause = isPause;
        m_pauseItems.SetActive(isPause);
        m_ballsController.FreezeAll(isPause);
        m_gameplayItems.SetActive(!isPause);
    }
    public void StartLevel()
    {

        SetPause(false);
        m_ballsController.Reset();
        m_blocksController.CreateLevel();
    }
}