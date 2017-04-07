using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : User
{
    public Player(IMessagesBox messageBox, string name)
        : base(messageBox, name)
    {
        messageBox.AddPlayerTurnEvent(OnPlayerTurn);
    }

    bool m_isTurnOn = false;

    public void OnPlayerTurn()
    {
        Debug.Log("Player set turn");
        m_isTurnOn = true;
    }
    public override void SetNewTurn(int state)
    {
        m_state = state;
        m_replics = m_replicsManager.GetPlayerReplics(m_state);
        m_isTurnOn = false;
    }
    protected override void WorkWithMessages(float delta)
    {
        Debug.Log("player send message");
    }
    protected override void TrySendNewMessage()
    {

    }
    protected override bool SendPredicate()
    {
        return m_isTurnOn;
    }
}
