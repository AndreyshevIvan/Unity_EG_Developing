using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public GameObject m_pauseItems;
    public GameObject m_gameplayItems;

    public Platform m_platform;
    public BallsController m_ballsController;
    public BlocksController m_blocksController;
    public BonusController m_bonusController;
    public AbstractUser m_player;
    public SwitchScenesCommands m_sceneSwithcer;
    public InfoController m_info;

    bool m_isPause = false;
    bool m_isGameStart = false;
    float m_gameOverColdown = 0;
    float m_buttonsColdown = 0;

    const float GAME_OVER_DELAY = 1;
    const float BUTTONS_COLDOWN = 0.2f;

    private void Start()
    {
        StartLevel();
    }
    public void StartLevel()
    {
        SetPause(false);
        m_ballsController.Reset();
        m_blocksController.CreateLevel();
    }
    public void StartNewLife()
    {
        m_isGameStart = false;
        m_ballsController.StartPlaying(m_isGameStart);
        m_player.StartPlaying(m_isGameStart);
        m_player.ResetToNextLife();
        m_ballsController.Reset();
        m_platform.Reset();
        m_bonusController.ClearBonuses();
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

        if (m_buttonsColdown < BUTTONS_COLDOWN)
        {
            m_buttonsColdown += Time.deltaTime;
        }
    }
    void HandlePauseEvents()
    {
        if (IsKeyPressed(KeyCode.Escape))
        {
            SetPause(!m_isPause);
        }
    }
    void HandleGameplayEvents()
    {
        if (Input.GetMouseButtonDown(0) && !m_isGameStart)
        {
            m_isGameStart = true;
            m_ballsController.StartPlaying(m_isGameStart);
            m_player.StartPlaying(m_isGameStart);
        }
        if (IsKeyPressed(KeyCode.Escape))
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
            m_info.TryOpenNewLevel();

            m_sceneSwithcer.SetWinScene();
        }
    }

    public void SetGameoverScene()
    {
        m_gameOverColdown += Time.deltaTime;

        if (m_gameOverColdown >= GAME_OVER_DELAY)
        {
            m_sceneSwithcer.SetGameoverScene();
        }
    }
    public void SetPause(bool isPause)
    {
        m_isPause = isPause;
        m_pauseItems.SetActive(isPause);
        m_ballsController.PauseBalls(isPause);
        m_gameplayItems.SetActive(!isPause);
        m_bonusController.SetFreeze(isPause);
        m_player.SetPause(isPause);
    }

    bool IsKeyPressed(KeyCode code)
    {
        if (m_buttonsColdown >= BUTTONS_COLDOWN)
        {
            if (Input.GetKeyDown(code))
            {
                m_buttonsColdown = 0;
                return true;
            }
        }
        return false;
    }
}