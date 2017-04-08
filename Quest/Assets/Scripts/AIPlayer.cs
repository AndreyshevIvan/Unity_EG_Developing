using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : User
{
    public AIPlayer(IMessagesBox messageBox, string name)
        : base(messageBox, name)
    {
    }

    const float ONE_CHAR_COLDOWN = 0.15f;

    public override void SetNewTurn(int state)
    {
        m_state = state;
        m_replics = m_replicsManager.GetComputerReplics(m_state);
        m_coldown = CalculateColdown(m_replics);
    }
    protected override void WorkWithMessages(float delta)
    {
        if (m_coldown < 0)
        {
            m_messageBox.SetComputerReplica(m_replics[0]);
            m_replics.Remove(m_replics[0]);
            m_coldown = CalculateColdown(m_replics);
        }
        else
        {
            m_messageBox.ImitatePrint();
            m_coldown -= Time.deltaTime;
        }
    }
    protected override void TrySendNewMessage()
    {

    }
    protected override bool SendPredicate()
    {
        return m_replics.Count == 0;
    }

    float CalculateColdown(List<string> replics)
    {
        if (replics.Count != 0)
        {
            return replics[0].Length * ONE_CHAR_COLDOWN;
        }

        return 0;
    }
}
