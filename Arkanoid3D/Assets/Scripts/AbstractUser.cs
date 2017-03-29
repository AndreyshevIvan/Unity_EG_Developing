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

    float m_fireballDuration = 0;
    float m_attackDuration = 0;
    float m_wallDuration = 0;

    int m_points = 0;
    int m_ballsCount = 1;
    int m_health = 1;
    int m_multiplitter = 1;

    const float MAX_FIREBALL_DUR = 4;
    const float MAX_ATTACK_DUR = 4;
    const int MAX_HEALTH = 3;
    const float MAX_WALL_DUR = 8;

    public void Start()
    {
        m_multiplitter = 1;
        m_health = MAX_HEALTH;
        m_UIController.Init(m_health);

        SetWallActive(false);
    }
    public void ResetToNextLife()
    {
        m_multiplitter = 1;
        SetWallActive(false);
    }

    void FixedUpdate()
    {
        UpdateData();

        UpdateWall();
        UpdateFireballMode();
        UpdateAttackMode();

        UpdateUI();
    }
    void UpdateData()
    {
        m_ballsCount = m_ballsController.GetBallsCount();
    }
    void UpdateUI()
    {
        m_UIController.UpdateWall(m_wallDuration, MAX_WALL_DUR);
        m_UIController.UpdatePoints(m_points);
        m_UIController.UpdateLife(m_health);
        m_UIController.UpdateBalls(m_ballsCount);
        m_UIController.UpdateMultiplier(m_multiplitter);
    }

    void UpdateFireballMode()
    {
        if (m_isFireballMode)
        {
            m_fireballDuration += Time.deltaTime;

            if (m_fireballDuration >= MAX_FIREBALL_DUR)
            {
                SetFireBallsMode(false);
            }
        }
    }
    public void SetFireBallsMode(bool isFireModeOn)
    {
        m_isFireballMode = isFireModeOn;
        m_ballsController.SetFireMode(isFireModeOn);

        if (m_isFireballMode)
        {
            m_fireballDuration = 0;
        }
    }

    void UpdateAttackMode()
    {
        if (m_isAttackMode)
        {
            m_attackDuration += Time.deltaTime;

            if (m_attackDuration >= MAX_ATTACK_DUR)
            {
                SetAttackMode(false);
            }
        }
    }
    public void SetAttackMode(bool isAttack)
    {
        m_isAttackMode = isAttack;
        m_platform.SetAttackMode(m_isAttackMode);

        if (!m_isAttackMode)
        {
            m_attackDuration = 0;
        }
    }

    public void AddMultiplier()
    {
        m_multiplitter++;
    }

    public void MuliplyBalls()
    {
        m_ballsController.DoubleAll();
    }

    public void SetWallActive(bool isActive)
    {
        m_isWallActive = isActive;
        m_wall.SetActive(isActive);

        if (!m_isWallActive)
        {
            m_wallDuration = 0;
        }
    }
    void UpdateWall()
    {
        if (m_isWallActive)
        {
            m_wallDuration += Time.deltaTime;

            if (m_wallDuration >= MAX_WALL_DUR)
            {
                SetWallActive(false);
            }
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
            MuliplyBalls();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWallActive(!m_isWallActive);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddMultiplier();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetAttackMode(!m_isAttackMode);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetFireBallsMode(!m_isFireballMode);
        }
    }

    public void WinEvents()
    {
        m_info.SetLevelPoints(m_points);
        m_info.AddToTotal(m_points);
        m_info.TrySetMaxPoints(m_points);
    }
}
