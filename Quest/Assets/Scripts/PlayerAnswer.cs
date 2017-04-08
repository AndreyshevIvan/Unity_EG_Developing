using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnswer : MonoBehaviour
{
    public delegate void OnAnswerEvent(string answerText, int newState);
    public OnAnswerEvent answerEvents;

    Text m_text;
    LayoutElement m_layoutElement;
    RectTransform m_transform;
    PlayerReplica m_replica;

    const float MAX_RELATIVE_WIDTH = 0.9f;
    const float RELATIVE_FONT_SIZE = 0.03f;

    void Awake()
    {
        m_text = GetComponentInChildren<Text>();
        m_layoutElement = GetComponentInChildren<LayoutElement>();
        m_transform = GetComponent<RectTransform>();
    }

    public void Init(PlayerReplica replica)
    {
        m_replica = replica;
        SetText(replica.toButton);
        UpdateLayout();
    }

    public void AnswerEvent()
    {
        answerEvents(m_replica.toSend, m_replica.nextState);
    }

    void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_transform);

        Vector2 newSize = m_transform.rect.size;

        if (newSize.x > Screen.width)
        {
            m_layoutElement.preferredWidth = Screen.width * MAX_RELATIVE_WIDTH;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(m_transform);
    }
    void SetText(string text)
    {
        m_text.text = text;
        m_text.fontSize = (int)(Screen.height * RELATIVE_FONT_SIZE);
    }
}