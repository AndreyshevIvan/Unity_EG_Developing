using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class User
{
    public User(IMessagesBox messagesBox, string name)
    {
        m_messageBox = messagesBox;
        m_name = name;
        m_replics = new List<string>();
    }

    protected string m_name;
    protected float m_coldown;
    protected int m_state;
    protected bool m_isTurnAllowed = false;

    protected List<string> m_replics;
    protected ReplicaController m_replicsManager;
    protected IMessagesBox m_messageBox;

    const float ENEMY_COLDOWN = 0.02f;

    public abstract void SetNewTurn(int state);
    protected abstract void WorkWithMessages(float delta);
    protected abstract void TrySendNewMessage();
    protected abstract bool SendPredicate();

    public bool SendMessage(float delta)
    {
        UpdateTimer(delta);
        WorkWithMessages(delta);
        TrySendNewMessage();

        return SendPredicate();
    }

    void UpdateTimer(float delta)
    {
        if (m_coldown >= 0)
        {
            m_coldown -= delta;
        }
    }

    public void InitReplicsManager(ReplicaController replics)
    {
        m_replicsManager = replics;
    }
}