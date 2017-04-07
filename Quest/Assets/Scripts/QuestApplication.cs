using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestApplication : MonoBehaviour
{
    public Chat m_chat;
    public UIManager m_UIManager;

    bool m_isInit = false;

    void Start()
    {
        LoadHistories();
    }
    void LoadHistories()
    {

    }
    void InitOnce()
    {
        if (!m_isInit)
        {
            m_isInit = true;
            m_chat.Init(m_UIManager);
        }
    }

    void FixedUpdate()
    {
        if (m_UIManager.isLoadEnded)
        {
            InitOnce();
            UpdateApplication();
        }
        else
        {
            // Load event
        }
    }
    void UpdateApplication()
    {
        m_chat.Update(Time.deltaTime);
    }
}
