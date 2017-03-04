using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameBehaviour m_currentBehaviour;

    GameBehaviour m_gameplay;
    GameBehaviour m_pause;
    GameBehaviour m_menu;
    GameBehaviour m_changeLevel;
    GameBehaviour m_gameover;
    GameBehaviour m_exit;

    public GameplayBehaviour m_gameplayBehaviour;
    public PauseBehaviour m_pauseBehavior;
    public MenuBehaviour m_menuBehaviour;
    public ChangeLevelBehaviour m_changeLevelBehavior;
    public GameoverBehaviour m_gameoverBehavior;
    public ExitBehaviour m_exitBehavior;

    public GameItems m_items;

    private void Awake()
    {
        InitBehaviours();

        m_currentBehaviour = m_gameplay;
    }

    private void InitBehaviours()
    {
        m_gameplay = m_gameplayBehaviour;
        m_pause = m_pauseBehavior;
        m_menu = m_menuBehaviour;
        m_changeLevel = m_changeLevelBehavior;
        m_gameover = m_gameoverBehavior;
        m_exit = m_exitBehavior;

        m_gameplayBehaviour.Init(this);
        m_pauseBehavior.Init(this);
        m_menuBehaviour.Init(this);
        m_changeLevelBehavior.Init(this);
        m_gameoverBehavior.Init(this);
        m_exitBehavior.Init(this);
    }

    void FixedUpdate()
    {
        m_currentBehaviour.UpdateBehavior();
    }

    public GameItems GetItems()
    {
        return m_items;
    }
}
