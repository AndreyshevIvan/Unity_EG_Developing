using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : User
{
    public AIPlayer(IMessagesBox messageBox, string name)
        : base(messageBox, name)
    {
    }

    public override void SetNewTurn(int state)
    {
        m_state = state;
        m_replics = m_replicsManager.GetComputerReplics(m_state);
    }
    protected override void WorkWithMessages(float delta)
    {
        Debug.Log("enemy send message");
    }
    protected override void TrySendNewMessage()
    {

    }
    protected override bool SendPredicate()
    {
        return true;
    }
}
