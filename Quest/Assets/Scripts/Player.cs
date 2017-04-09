using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : User
{
    public Player(IMessagesBox messageBox, string name)
        : base(messageBox, name)
    {
        messageBox.AddPlayerTurnEvent(SetPlayerTurn);
        m_isImitate = true;
    }

    void SetPlayerTurn(int state, string message)
    {
        SetLastMessage(message);
        m_history.AddPlayerReplica(message, m_state);
        m_isTurnAllowed = true;
    }
    public override void SetNewTurn(int state)
    {
        m_state = state;
        m_replics = m_replicsManager.GetPlayerReplics(m_state);
        if (m_replics.Count != 0)
        {
            m_messageBox.SetPlayerReplics(m_replics);
            m_isTurnAllowed = false;
        }
    }
    protected override void WorkWithMessages(float delta)
    {
    }
    protected override void TrySendNewMessage()
    {
    }
    protected override bool SendPredicate()
    {
        return m_isTurnAllowed;
    }
}
