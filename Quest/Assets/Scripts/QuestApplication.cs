using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestApplication : MonoBehaviour
{
    public Chat m_chat;
    public UIManager m_UIManager;

    string m_chatName = "testChat";
    bool m_isInit = false;

    void Start()
    {
        LoadHistories();
    }
    void LoadHistories()
    {
        m_UIManager.Load(m_chatName);
    }
    void InitOnce()
    {
        if (!m_isInit)
        {
            m_isInit = true;
            m_chat = new Chat(m_UIManager, m_chatName);
        }
    }

    void FixedUpdate()
    {
        InitOnce();
        m_chat.Update(Time.deltaTime);
    }
}
