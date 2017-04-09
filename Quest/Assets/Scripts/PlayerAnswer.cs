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
    UserReplica m_replica;

    const float MAX_RELATIVE_WIDTH = 0.8f;
    const float RELATIVE_FONT_SIZE = 0.027f;

    void Awake()
    {
        m_text = GetComponentInChildren<Text>();
        m_layoutElement = GetComponentInChildren<LayoutElement>();
        m_transform = GetComponent<RectTransform>();
    }

    public void Init(UserReplica replica, OnAnswerEvent answerEvent)
    {
        answerEvents += answerEvent;
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
        RectTransform parentTransform = GetComponentInParent<RectTransform>();

        if (newSize.x > Screen.width * MAX_RELATIVE_WIDTH)
        {
            m_layoutElement.preferredWidth = Screen.width * MAX_RELATIVE_WIDTH;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(m_transform);
    }

    public void SetParent(RectTransform parent)
    {
        transform.SetParent(parent);
    }
    public void SetPosition(Vector3 position)
    {
        m_transform.localPosition = position;
    }
    void SetText(string text)
    {
        m_text.text = text;
        m_text.fontSize = (int)(Screen.height * RELATIVE_FONT_SIZE);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}