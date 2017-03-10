using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractUser : MonoBehaviour
{
    public GameObject m_wall;

    public UIController m_UIController;
    public BallsController m_ballsController;
    public BlocksController m_blocksController;
    public Platform m_platform;

    bool m_isFireballMode = false;
    float m_fireballDuration = 0;
    float m_maxFireballDuration = 4;

    bool m_isAttackMode = false;
    float m_attackDuration = 0;
    float m_maxAttackDuration = 4;

    float m_multiplitterDuration = 4;
    float m_maxMultiplitterDuration = 4;
    int m_multiplitter = 1;

    bool m_isTimeScale = false;
    float m_effectTimeScale = 0.4f;
    float m_currScaleDuration = 0;
    float m_maxScaleDuration = 5;

    int m_maxHealth = 3;

    bool m_isWallActive = false;
    float m_wallDuration = 8;
    float m_currWallDuration = 0;

    int m_points;
    int m_ballsCount;

    int m_life;

    private void Awake()
    {

    }

    public void Start()
    {
        m_life = 3;
        SetWallActive(false);
    }
    public void ResetToNextLife()
    {
        SetWallActive(false);
    }

    void FixedUpdate()
    {
        UpdateData();

        UpdateWall();
        UpdateTimeScale();
        UpdateMultiplitter();
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
        m_UIController.UpdateWall(m_currWallDuration, m_wallDuration);
        m_UIController.UpdatePoints(m_points);
        m_UIController.UpdateLife(m_life);
        m_UIController.UpdateBalls(m_ballsCount);
        m_UIController.UpdateTimeScale(m_currScaleDuration, m_maxScaleDuration);
        m_UIController.UpdateMultiplitter(m_multiplitter);
    }

    void UpdateFireballMode()
    {
        if (m_isFireballMode)
        {
            m_fireballDuration += Time.deltaTime;

            if (m_fireballDuration >= m_maxFireballDuration)
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

            if (m_attackDuration >= m_maxAttackDuration)
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

    public void AddMultiplitter()
    {
        m_multiplitterDuration = 0;
        m_multiplitter++;
    }
    void UpdateMultiplitter()
    {
        if (m_multiplitterDuration < m_maxMultiplitterDuration)
        {
            m_multiplitterDuration += Time.deltaTime;
        }
        else
        {
            m_multiplitter = 1;
        }
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
            m_currWallDuration = 0;
        }
    }
    void UpdateWall()
    {
        if (m_isWallActive)
        {
            m_currWallDuration += Time.deltaTime;

            if (m_currWallDuration >= m_wallDuration)
            {
                SetWallActive(false);
            }
        }
    }

    public void SetTimeScale(bool isStart)
    {
        m_isTimeScale = isStart;

        if (!m_isTimeScale)
        {
            Time.timeScale = 1;
            m_currScaleDuration = 0;
        }
    }
    void UpdateTimeScale()
    {
        if (m_isTimeScale)
        {
            Time.timeScale = m_effectTimeScale;
            m_currScaleDuration += Time.deltaTime;

            if (m_currScaleDuration >= m_maxScaleDuration)
            {
                SetTimeScale(false);
            }
        }
    }

    public bool IsPlayerLive()
    {
        return (m_life > 0);
    }
    public void AddLife()
    {
        if (m_life < m_maxHealth)
        {
            m_life++;
        }
    }
    public void ReduceLife()
    {
        m_life--;
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetTimeScale(!m_isTimeScale);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWallActive(!m_isWallActive);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddMultiplitter();
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
        PlayerPrefs.SetInt("Points", m_points);
        SetTimeScale(false);
    }
}
