using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameBehavior m_currentBehavior;
    GameBehavior m_gameplay;
    GameBehavior m_pause;

    public GameplayBehavior m_gameplayBehavior;
    public PauseBehavior m_pauseBehavior;

    private void Awake()
    {
        m_gameplayBehavior.Init(this);
        m_pauseBehavior.Init(this);

        m_gameplay = m_gameplayBehavior;
        m_pause = m_pauseBehavior;

        m_currentBehavior = m_gameplay;
    }

    void FixedUpdate()
    {
        m_currentBehavior.UpdateBehavior();
    }

    public void SwitchBehavior()
    {
        m_currentBehavior = (m_currentBehavior == m_gameplay) ? m_pause : m_gameplay;
    }
}
