using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{

    public GameObject m_pauseItems;
    public GameObject m_gameplayItems;

    bool m_isPause = false;

    public Platform m_platform;
    public BallsController m_ballsController;
    public BlocksController m_blocksController;
    public BonusController m_bonusController;
    public AbstractUser m_player;
    public SwitchScenesCommands m_sceneSwithcer;

    public InfoController m_info;

    int m_levelNumber;

    private void Awake()
    {
        StartLevel();
    }
    public void StartNewLife()
    {
        m_player.ResetToNextLife();
        m_ballsController.Reset();
        m_platform.Reset();
        m_bonusController.ClearBonuses();
    }
    public void StartLevel()
    {
        m_levelNumber = m_info.GetSpawnLevel();

        SetPause(false);
        m_ballsController.Reset();
        m_blocksController.CreateLevel();
    }

    void FixedUpdate()
    {
        if (m_isPause)
        {
            HandlePauseEvents();
            PauseUpdate();
        }
        else
        {
            HandleGameplayEvents();
            GameUpdate();
        }
    }
    void HandlePauseEvents()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!m_isPause);
        }
    }
    void HandleGameplayEvents()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!m_isPause);
        }
        m_platform.HandleEvents();
        m_player.HandleCheats();
    }
    void PauseUpdate()
    {

    }
    void GameUpdate()
    {
        CheckPlayerLife();
        CheckWin();
    }

    void CheckPlayerLife()
    {
        if (m_ballsController.GetBallsCount() <= 0)
        {
            m_player.ReduceLife();

            if (m_player.IsPlayerLive())
            {
                StartNewLife();
            }
            else
            {
                SetGameoverScene();
            }
        }
    }
    void CheckWin()
    {
        if (m_blocksController.GetBlocksCount(false) == 0)
        {
            m_player.WinEvents();
            UnlockNewLevel();

            m_sceneSwithcer.SetWinScene();
        }
    }
    void UnlockNewLevel()
    {
        int newLevelsCount = m_levelNumber + 1;

        if (m_info.GetMaxLevelsCount() >= newLevelsCount)
        {
            m_info.SetOpenLevelsCount(newLevelsCount);
        }
    }

    public void SetGameoverScene()
    {
        m_sceneSwithcer.SetGameoverScene();
    }
    public void SetPause(bool isPause)
    {
        m_isPause = isPause;
        m_pauseItems.SetActive(isPause);
        m_ballsController.PauseBalls(isPause);
        m_gameplayItems.SetActive(!isPause);
        m_bonusController.SetFreeze(isPause);
    }
}