using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractUser : MonoBehaviour
{
    public GameObject m_wall;

    public GameplayUIController m_UIController;
    public BallsController m_ballsController;
    public BlocksController m_blocksController;
    public Platform m_platform;

    public InfoController m_info;

    bool m_isFireballMode = false;
    bool m_isAttackMode = false;
    bool m_isWallActive = false;
    bool m_isPause = false;
    bool m_isGameStart = false;

    float m_fireballDuration = FIREBALL_DUR;
    float m_attackDuration = ATTACK_DUR;
    float m_wallDuration = WALL_DUR;
    float m_gameTime = 0;

    int m_points = 0;
    int m_health = 1;
    int m_multiplitter = 1;

    const float FIREBALL_DUR = 4;
    const float ATTACK_DUR = 4;
    const float WALL_DUR = 8;
    const int MAX_HEALTH = 3;
    const int MAX_MULTIPLIER = 99;


    public void Start()
    {
        m_multiplitter = 1;
        m_health = MAX_HEALTH;
        m_UIController.Init(m_health);
        m_isGameStart = false;

        SetWallActive(false);
    }
    public void ResetToNextLife()
    {
        m_multiplitter = 1;
        SetWallActive(false);
    }
    public void StartPlaying(bool isGameStart)
    {
        m_isGameStart = isGameStart;
        m_UIController.StartPlaying(m_isGameStart);
    }
    public void SetPause(bool isPause)
    {
        m_isPause = isPause;
    }


    void FixedUpdate()
    {
        if (!m_isPause)
        {
            UpdateWall();
            UpdateFireballMode();
            UpdateAttackMode();
            UpdateGameTime();
            UpdateUI();
        }
    }
    void UpdateUI()
    {
        m_UIController.UpdatePoints(m_points);
        m_UIController.UpdateMultiplier(m_multiplitter);
        m_UIController.UpdateLife(m_health);
        m_UIController.UpdateTime(m_gameTime);
    }
    void UpdateGameTime()
    {
        if (m_isGameStart)
        {
            m_gameTime += Time.deltaTime;
        }
    } 


    void UpdateWall()
    {
        if (m_isWallActive)
        {
            m_wallDuration += Time.deltaTime;

            if (m_wallDuration >= WALL_DUR)
            {
                SetWallActive(false);
            }
        }
    }
    void UpdateFireballMode()
    {
        if (m_isFireballMode)
        {
            m_fireballDuration += Time.deltaTime;

            if (m_fireballDuration >= FIREBALL_DUR)
            {
                SetFireBallsMode(false);
            }
        }
    }
    void UpdateAttackMode()
    {
        if (m_isAttackMode)
        {
            m_attackDuration += Time.deltaTime;

            if (m_attackDuration >= ATTACK_DUR)
            {
                SetAttackMode(false);
            }
        }
    }


    public void SetFireBallsMode(bool isFireModeOn)
    {
        m_isFireballMode = isFireModeOn;
        m_ballsController.SetFireMode(isFireModeOn);

        if (m_isFireballMode)
        {
            m_UIController.SetFireBallPlate(FIREBALL_DUR);
            m_fireballDuration = 0;
        }
    }
    public void SetAttackMode(bool isAttack)
    {
        m_isAttackMode = isAttack;
        m_platform.SetAttackMode(m_isAttackMode);

        if (m_isAttackMode)
        {
            m_UIController.SetAttackPlate(ATTACK_DUR);
            m_attackDuration = 0;
        }
    }
    public void SetWallActive(bool isActive)
    {
        m_isWallActive = isActive;
        m_wall.SetActive(isActive);

        if (m_isWallActive)
        {
            m_UIController.SetWallPlate(WALL_DUR);
            m_wallDuration = 0;
        }
    }
    public void AddMultiplier()
    {
        if (m_multiplitter < MAX_MULTIPLIER)
        {
            m_UIController.SetMultiplierPlate();
            m_multiplitter++;
        }
    }
    public void MuliplyBalls()
    {
        if (m_ballsController.DoubleAll())
        {
            m_UIController.SetMultiBallsPlate();
        }
    }


    public bool IsPlayerLive()
    {
        return (m_health > 0);
    }
    public void AddLife()
    {
        if (m_health < MAX_HEALTH)
        {
            m_health++;
        }
    }
    public void ReduceLife()
    {
        if (m_health > 0)
        {
            m_health--;
        }
    }


    public void AddPoints(int points)
    {
        m_points += points * m_multiplitter;
    }
    public void ReducePoints(int reducePoints)
    {
        m_points -= reducePoints;
    }


    public void HandleCheats()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWallActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetAttackMode(!m_isAttackMode);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetFireBallsMode(!m_isFireballMode);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            MuliplyBalls();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AddMultiplier();
        }
    }
    public void WinEvents()
    {
        m_info.SetLevelTime((int)m_gameTime);
        m_info.SetLevelPoints(m_points);
        m_info.AddToTotal(m_points);
        m_info.TrySetMaxPoints(m_points);
    }
}
