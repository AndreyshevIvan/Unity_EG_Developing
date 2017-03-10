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

    public int m_ballsLayer;
    public int m_blocksLayer;
    public int m_bonusesLayer;
    public int m_platformLayer;
    public int m_borderLayer;

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
        if (Input.GetKey(KeyCode.Escape))
        {
            SetPause(!m_isPause);
        }
    }
    void HandleGameplayEvents()
    {
        m_platform.HandleEvents();
        m_player.HandleCheats();
    }
    void PauseUpdate()
    {

    }
    void GameUpdate()
    {
        CheckPlayerLife();
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

    public void SetGameoverScene()
    {
        SceneManager.LoadScene("Scenes/Gameover");
    }
    public void SetPause(bool isPause)
    {
        m_isPause = isPause;
        m_pauseItems.SetActive(isPause);
        m_ballsController.FreezeAll(isPause);
        m_gameplayItems.SetActive(!isPause);
        m_bonusController.SetFreeze(isPause);
    }
}