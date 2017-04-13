using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MessageSide
{
    LEFT,
    RIGHT,
}

public class MessagesBox : MonoBehaviour, IMessagesBox
{
    PlayerTurnEvents m_playerTurnEvents;

    List<Message> m_boxMessages;
    public Message m_computerMessage;
    public Message m_playerMessage;
    Message m_imitateMessage;
    bool m_isImitate = false;
    bool m_isAwake = false;

    public PlayerAnswer m_answer;
    List<PlayerAnswer> m_lastAnswers;

    public RectTransform m_listTransform;
    public RectTransform m_answersBox;
    public RectTransform m_answersInner;
    public RectTransform m_listLayout;

    Vector3 m_normalBoxPosition;
    Vector3 m_listStartSize;
    Vector3 m_startListLayoutPos;
    Vector3 m_answersBoxStartSize;
    Vector3 m_answersBoxStartPos;
    float m_messagesBoxHeight = 0;

    RectTransform m_transform;

    readonly float RELATIVE_MESSAGE_OFFSET = 20;
    readonly float RELATIVE_ANSWERS_OFFSET = 18;
    readonly Vector3 INVISIBLE_POSITION = new Vector3(1000000, 0, 0);

    public PlayerTurnEvents playerTurnEvent
    {
        set { m_playerTurnEvents = value; }
    }

    void Awake()
    {
        m_isAwake = true;
        m_normalBoxPosition = transform.position;
        m_playerTurnEvents = new PlayerTurnEvents();
        m_lastAnswers = new List<PlayerAnswer>();
        m_boxMessages = new List<Message>();
        m_transform = GetComponent<RectTransform>();

        m_answersBoxStartSize = m_answersBox.rect.size;
        m_answersBoxStartPos = m_answersBox.localPosition;

        m_startListLayoutPos = m_listLayout.localPosition;

        IncreaseMessageLine(RELATIVE_MESSAGE_OFFSET);
    }

    public void LoadFromHistory(History history)
    {
        if (!m_isAwake)
        {
            Awake();
        }

        m_listTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

        foreach (Message message in m_boxMessages)
        {
            if (message != null)
            {
                Destroy(message.gameObject);
            }
        }

        List<Pair<MessageSide, string>> historyMesages = history.GetReplics();
        foreach (Pair<MessageSide, string> replica in historyMesages)
        {
            Message message = null;

            if (replica.first == MessageSide.LEFT)
            {
                message = Instantiate(m_computerMessage);
            }
            else
            {
                message = Instantiate(m_playerMessage);
            }

            AddMessage(message, replica.second);
        }
    }
    public void ImitatePrint()
    {
        if (m_isImitate)
        {
            return;
        }

        m_imitateMessage = Instantiate(m_computerMessage);
        m_imitateMessage.SetText("...");
        IncreaseMessageLine(m_imitateMessage.GetHeight());
        m_imitateMessage.UpdateTransform(m_listTransform);
        IncreaseMessageLine(RELATIVE_MESSAGE_OFFSET);
        m_isImitate = true;
    }
    public void InitPlayerAnswers(List<UserReplica> replics)
    {
        m_lastAnswers.Clear();

        foreach (UserReplica replica in replics)
        {
            PlayerAnswer answer = Instantiate(m_answer);
            answer.Init(replica, AddPlayerMessage);
            m_lastAnswers.Add(answer);
        }

        UpdateAnswersBox();
    }
    public void AddPlayerTurnEvent(PlayerEvent turnEvent)
    {
        m_playerTurnEvents.AddEvent(ref turnEvent);
    }
    public void AddComputerMessage(UserReplica replica)
    {
        Message message = Instantiate(m_computerMessage);
        AddMessage(message, replica.toSend);
    }
    public void SetVisible(bool isVisible)
    {
        if (isVisible)
        {
            transform.position = new Vector3(m_normalBoxPosition.x, transform.position.y, 0);
        }
        else
        {
            transform.position = new Vector3(INVISIBLE_POSITION.x, transform.position.y, 0);
        }
    }

    void AddPlayerMessage(string text, int newState)
    {
        m_playerTurnEvents.DoEvents(newState, text);
        Message message = Instantiate(m_playerMessage);
        AddMessage(message, text);
        ClearLastAnswers();
        ResetAnswersBox();
    }
    void AddMessage(Message message, string text)
    {
        StopImitate();

        message.SetText(text);
        IncreaseMessageLine(message.GetHeight());
        message.UpdateTransform(m_listTransform);
        IncreaseMessageLine(RELATIVE_MESSAGE_OFFSET);
        m_boxMessages.Add(message);
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

        m_listLayout.position = new Vector3(m_listLayout.position.x, height, m_listLayout.position.z);
    }
    void ResetAnswersBox()
    {
        m_answersBox.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, m_answersBoxStartSize.y);
        m_listLayout.localPosition = new Vector3(m_listLayout.localPosition.x, m_startListLayoutPos.y);
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
