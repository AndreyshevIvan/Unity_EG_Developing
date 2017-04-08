using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MessageSide
{
    LEFT,
    RIGHT,
}

public class MessagesBox : MonoBehaviour
{
    PlayerTurnEvents m_playerTurnEvents;

    public Message m_computerMessage;
    public Message m_playerMessage;
    public RectTransform m_boxTransform;

    public PlayerAnswer m_answer;
    public RectTransform m_answersBox;

    Message m_lastReplica;
    Vector3 m_lastPosition = Vector3.zero;
    float m_mesagesHeight = 0;

    public PlayerTurnEvents playerTurnEvent
    {
        set { m_playerTurnEvents = value; }
    }

    public void ImitatePrint()
    {

    }

    public void AddComputerMessage(string text)
    {
        Message message = Instantiate(m_computerMessage);
        AddMessage(message, text, MessageSide.LEFT);
        Debug.Log("Send by enemy ended");
    }
    public void AddPlayerReplics(List<PlayerReplica> replics)
    {
        // instantiate buttons
        // update transform
        // update field size
        // add buttons to field
        // add delegates to buttons
    }
    void SendPlayerMessage(int newState, string text)
    {
        m_playerTurnEvents.DoEvents(newState);
        Message message = Instantiate(m_playerMessage);
        AddMessage(message, text, MessageSide.RIGHT);
    }
    public void AddMessage(Message message, string text, MessageSide side)
    {
        if (m_lastReplica != null)
        {
            m_lastPosition = m_lastReplica.GetDownLeftPosition();
        }

        message.SetText(text);
        message.UpdateLayout();
        m_mesagesHeight += message.GetHeight();
        m_boxTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_mesagesHeight);
        message.SetTransformProperty(m_boxTransform, m_lastPosition, side);
        m_lastReplica = message;
    }
}
