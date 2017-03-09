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
    public PlayerController m_player;

    public int m_ballsLayer;
    public int m_blocksLayer;
    public int m_bonusesLayer;
    public int m_platformLayer;

    private void Awake()
    {
        StartLevel();
        SetPhysicsOptions();
    }
    public void StartNewLife()
    {
        m_player.ResetToNextLife();
        m_ballsController.Reset();
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

    void SetPhysicsOptions()
    {
        Physics.IgnoreLayerCollision(m_ballsLayer, m_ballsLayer);
        Physics.IgnoreLayerCollision(m_bonusesLayer, m_blocksLayer);
        Physics.IgnoreLayerCollision(m_bonusesLayer, m_ballsLayer);
        Physics.IgnoreLayerCollision(m_bonusesLayer, m_bonusesLayer);
        Physics.IgnoreLayerCollision(m_bonusesLayer, m_bonusesLayer);
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