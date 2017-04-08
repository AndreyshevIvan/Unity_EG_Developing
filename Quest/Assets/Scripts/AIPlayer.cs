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
        m_isTurnAllowed = true;
    }
    protected override void WorkWithMessages(float delta)
    {
        m_isTurnAllowed = (m_replics.Count != 0);

        if (m_isTurnAllowed)
        {
            Debug.Log("Send by enemy");
            m_messageBox.SetComputerReplica(m_replics[0]);
            m_replics.Remove(m_replics[0]);
        }
    }
    protected override void TrySendNewMessage()
    {

    }
    protected override bool SendPredicate()
    {
        return !m_isTurnAllowed;
    }
}
