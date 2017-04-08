using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : User
{
    public Player(IMessagesBox messageBox, string name)
        : base(messageBox, name)
    {
        messageBox.AddPlayerTurnEvent(SetPlayerTurn);
    }

    List<PlayerReplica> m_playerReplics;

    void SetPlayerTurn(int state)
    {
        m_isTurnAllowed = true;
    }
    public override void SetNewTurn(int state)
    {
        m_state = state;
        m_playerReplics = m_replicsManager.GetPlayerReplics(m_state);
        m_messageBox.SetPlayerReplics(m_playerReplics);
        m_isTurnAllowed = false;
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
