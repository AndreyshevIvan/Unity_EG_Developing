using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class User
{
    public User(IMessagesBox messagesBox, string name)
    {
        m_messageBox = messagesBox;
        m_name = name;
        m_replics = new List<UserReplica>();
    }

    protected string m_name;
    protected float m_writeColdown = 0;
    protected float m_waitColdown = 0;
    protected int m_state;
    protected bool m_isTurnAllowed = false;
    protected bool m_isImitate = false;
    float m_lastMessageTime = 0;
    string m_lastMessage = "";

    protected List<UserReplica> m_replics;
    protected ReplicaController m_replicsManager;
    protected IMessagesBox m_messageBox;
    protected History m_history;

    bool isWaitColdownLeft
    {
        get { return (m_waitColdown <= 0); }
    }
    bool isWriteColdownLeft
    {
        get { return (m_writeColdown <= 0); }
    }

    const float ENEMY_COLDOWN = 0.02f;

    public abstract void SetNewTurn(int state);
    protected abstract void WorkWithMessages(float delta);
    protected abstract void TrySendNewMessage();
    protected abstract bool SendPredicate();

    public void SetHistory(History history)
    {
        m_history = history;
    }
    public bool SendMessage(float delta)
    {
        UpdateTimer(delta);

        if (isWaitColdownLeft && !isWriteColdownLeft)
        {
            WorkWithMessages(delta);
            
            if (m_isImitate)
            {
                m_messageBox.ImitatePrint();
            }
        }

        if (IsColdownsLeft())
        {
            TrySendNewMessage();
        }

        return SendPredicate();
    }
    protected void SetLastMessage(string replica)
    {
        m_lastMessageTime = Time.realtimeSinceStartup;
        m_lastMessage = replica;
    }

    void UpdateTimer(float delta)
    {
        if (m_waitColdown >= 0)
        {
            m_waitColdown -= delta;
        }
        else if (m_writeColdown >= 0)
        {
            m_writeColdown -= delta;
        }
    }

    bool IsColdownsLeft()
    {
        return isWaitColdownLeft && isWriteColdownLeft;
    }
    public void InitReplicsManager(ReplicaController replics)
    {
        m_replicsManager = replics;
    }
}