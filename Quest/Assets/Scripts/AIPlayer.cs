using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : User
{
    public AIPlayer(IMessagesBox messageBox, string name)
        : base(messageBox, name)
    {
        m_isImitate = true;
    }

    public delegate void OnTurnEvent();
    public OnTurnEvent turnEvent;

    const float ONE_CHAR_COLDOWN = 0.1f;

    public override void SetNewTurn(int state)
    {
        m_state = state;
        m_replics = m_replicsManager.GetComputerReplics(m_state);
        m_history.AddComputerReplica(m_replics, m_state);
        CalculateWriteColdowns(m_replics);
    }
    protected override void WorkWithMessages(float delta)
    {

    }
    protected override void TrySendNewMessage()
    {
        string message = m_replics[0].toSend;

        m_messageBox.AddComputerMessage(m_replics[0]);
        SetLastMessage(message);

        if (onSendMessage != null)
        {
            onSendMessage(m_name, message);
        }

        if (turnEvent != null)
        {
            turnEvent();
        }

        m_replics.Remove(m_replics[0]);
        CalculateWriteColdowns(m_replics);
    }
    protected override bool SendPredicate()
    {
        return (m_replics.Count == 0);
    }

    void CalculateWriteColdowns(List<UserReplica> replics)
    {
        if (replics.Count != 0)
        {
            m_waitColdown = replics[0].waitColdown;
            m_writeColdown = replics[0].toSend.Length * ONE_CHAR_COLDOWN;
        }
    }
}
