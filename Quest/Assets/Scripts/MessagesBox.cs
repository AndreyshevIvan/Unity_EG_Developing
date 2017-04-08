using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    Message m_imitateMessage;
    bool m_isImitate = false;

    public PlayerAnswer m_answer;
    List<PlayerAnswer> m_lastAnswers;

    public RectTransform m_listTransform;
    public RectTransform m_answersBox;
    public RectTransform m_answersInner;
    public RectTransform m_listLayout;
    public RectTransform m_titleTransform;

    Vector3 m_listStartSize;
    Vector3 m_startListLayoutPos;
    Vector3 m_answersBoxStartSize;
    Vector3 m_answersBoxStartPos;
    float m_messagesBoxHeight = 0;

    RectTransform m_transform;

    readonly float RELATIVE_MESSAGE_OFFSET = 0.02f * Screen.height;
    readonly float RELATIVE_ANSWERS_OFFSET = 0.01f * Screen.width;

    public PlayerTurnEvents playerTurnEvent
    {
        set { m_playerTurnEvents = value; }
    }

    private void Awake()
    {
        m_lastAnswers = new List<PlayerAnswer>();
        m_transform = GetComponent<RectTransform>();

        m_answersBoxStartSize = m_answersBox.rect.size;
        m_answersBoxStartPos = m_answersBox.localPosition;

        m_startListLayoutPos = m_listLayout.localPosition;

        IncreaseMessageLine(RELATIVE_MESSAGE_OFFSET);
    }
    
    public void ImitatePrint()
    {
        if (!m_isImitate)
        {
            m_imitateMessage = Instantiate(m_computerMessage);
            m_imitateMessage.SetText("...");
            IncreaseMessageLine(m_imitateMessage.GetHeight());
            m_imitateMessage.UpdateTransform(m_listTransform, MessageSide.LEFT);
            IncreaseMessageLine(RELATIVE_MESSAGE_OFFSET);
            m_isImitate = true;
        }
    }
    public void InitPlayerReplics(List<PlayerReplica> replics)
    {
        m_lastAnswers.Clear();

        foreach (PlayerReplica replica in replics)
        {
            PlayerAnswer answer = Instantiate(m_answer);
            answer.Init(replica, AddPlayerMessage);
            m_lastAnswers.Add(answer);
        }

        UpdateAnswersBox();
    }

    public void AddComputerMessage(string text)
    {
        Message message = Instantiate(m_computerMessage);
        AddMessage(message, text, MessageSide.LEFT);
    }
    void AddPlayerMessage(string text, int newState)
    {
        m_playerTurnEvents.DoEvents(newState);
        Message message = Instantiate(m_playerMessage);
        AddMessage(message, text, MessageSide.RIGHT);
        ClearLastAnswers();
        ResetAnswersBox();
    }
    void AddMessage(Message message, string text, MessageSide side)
    {
        StopImitate();

        message.SetText(text);
        IncreaseMessageLine(message.GetHeight());
        message.UpdateTransform(m_listTransform, side);
        IncreaseMessageLine(RELATIVE_MESSAGE_OFFSET);
    }
    void StopImitate()
    {
        if (m_isImitate)
        {
            ReduceMessageLine(m_imitateMessage.GetHeight());
            ReduceMessageLine(RELATIVE_MESSAGE_OFFSET);
            m_isImitate = false;    
            Destroy(m_imitateMessage.gameObject);
        }
    }

    void IncreaseMessageLine(float height)
    {
        m_messagesBoxHeight += height;
        m_listTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_messagesBoxHeight);
    }
    void ReduceMessageLine(float height)
    {
        m_messagesBoxHeight -= height;
        m_listTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_messagesBoxHeight);
    }

    void UpdateAnswersBox()
    {
        float height = RELATIVE_ANSWERS_OFFSET;
        List<Vector3> answerPositions = new List<Vector3>();

        foreach (PlayerAnswer answer in m_lastAnswers)
        {
            Vector3 answerSize = answer.GetComponent<RectTransform>().rect.size;
            Vector3 answerPos = new Vector3(0, -height, 0);
            answerPositions.Add(answerPos);
            answer.SetParent(m_answersInner);
            height += (RELATIVE_ANSWERS_OFFSET + answerSize.y);
        }

        m_answersBox.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, height);

        for (int i = 0; i < m_lastAnswers.Count; i++)
        {
            m_lastAnswers[i].SetPosition(answerPositions[i]);
        }

        float newLayoutHeight = m_transform.rect.size.y - height;
        m_listLayout.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newLayoutHeight);
        m_listTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_messagesBoxHeight);

        m_listLayout.transform.position = new Vector3(0, height, 0);
    }
    void ResetAnswersBox()
    {
        m_answersBox.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, m_answersBoxStartSize.y);
        m_listLayout.localPosition = m_startListLayoutPos;
    }
    void ClearLastAnswers()
    {
        foreach (PlayerAnswer answer in m_lastAnswers)
        {
            answer.Destroy();
        }
        m_lastAnswers.Clear();
    }
}
