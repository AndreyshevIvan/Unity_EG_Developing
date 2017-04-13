using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestApplication : MonoBehaviour
{
    public UIManager m_UIManager;
    public ChatAudio m_audio;

    public ChatIcon m_iconMari;
    public ChatIcon m_iconAgent;

    List<Chat> m_chats;
    List<string> m_keys;

    string m_mariChatName = "Mari";
    string m_agentChatName = "AgentCoop";

    bool m_isFastMode = false;

    const string START_AGENT_KEY = "AgentStart";

    private void Start()
    {
        m_chats = new List<Chat>();
        m_keys = new List<string>();

        m_UIManager.onCloseChats += DiactivateAllChats;
        InitChat(m_iconMari, m_mariChatName, m_UIManager.GetMariChatBox(), m_UIManager.OpenMariChat);
        m_UIManager.OpenRoomsPage(true);
    }
    void InitChat(ChatIcon icon, string name, IMessagesBox msgBox, ChatIcon.OnIconClick iconClickEvent)
    {
        icon.InitUI(m_UIManager);
        icon.onClickEvent += iconClickEvent;

        Chat chat = new Chat(msgBox, icon, m_audio, name);
        chat.onKeyEvent += OnKeyEvent;

        m_chats.Add(chat);
    } 

    public void SetFastMode()
    {
        m_isFastMode = !m_isFastMode;
        foreach (Chat chat in m_chats)
        {
            chat.SetColdownIgnore(m_isFastMode);
        }
        m_UIManager.SetColdownIgnore(m_isFastMode);
    }

    void LateUpdate()
    {
        List<Chat> chats = new List<Chat>();

        foreach (Chat chat in m_chats)
        {
            chats.Add(chat);
        }

        foreach (Chat chat in chats)
        {
            chat.Update(Time.deltaTime);
        }
    }

    void DiactivateAllChats()
    {
        foreach (Chat chat in m_chats)
        {
            chat.Diactivate();
        }
    }
    void OnDestroy()
    {
        foreach (Chat chat in m_chats)
        {
            if (chat != null)
            {
                History history = chat.GetHistory();
                DataManager.SaveHistory(history);
            }
        }
    }
    void OnKeyEvent(string key)
    {
        m_keys.Add(key);

        if (key == START_AGENT_KEY)
        {
            InitChat(m_iconAgent, m_agentChatName, m_UIManager.GetAgentChatBox(), m_UIManager.OpenAgentChat);
        }
    }

    public void RestartGame()
    {
        foreach (Chat chat in m_chats)
        {
            chat.GetHistory().Reset();
        }

        DataManager.ResetAll();
        SceneManager.LoadScene("Scenes/Login");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
