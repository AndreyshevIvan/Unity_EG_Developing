using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestApplication : MonoBehaviour
{
    public UIManager m_UIManager;

    public ChatIcon m_iconMari;
    Chat m_chatMari;

    string m_mariChat = "MariChat";
    bool m_isFastMode = false;

    private void Start()
    {
        InitChats();
        m_UIManager.OpenRoomsPage(true);
    }
    void InitChats()
    {
        m_UIManager.onCloseChats += DiactivateAllChats;

        m_iconMari.onClickEvent += m_UIManager.SetMariChat;

        m_chatMari = new Chat(m_UIManager, m_iconMari, m_mariChat);

        m_chatMari.onKeyEvent += OnKeyEvent;
    } 

    public void SetFastMode()
    {
        m_isFastMode = !m_isFastMode;
        m_chatMari.SetColdownIgnore(m_isFastMode);
        m_UIManager.SetColdownIgnore(m_isFastMode);
    }

    void LateUpdate()
    {
        m_chatMari.Update(Time.deltaTime);
    }

    void DiactivateAllChats()
    {
        Debug.Log("Diactivate all");

        m_chatMari.Diactivate();
    }
    void OnDestroy()
    {
        if (m_chatMari != null)
        {
            History history = m_chatMari.GetHistory();
            DataManager.SaveHistory(history);
        }
    }
    void OnKeyEvent(string key)
    {

    }

    public void RestartGame()
    {
        m_chatMari.GetHistory().Reset();
        DataManager.ResetAll(m_mariChat);
        SceneManager.LoadScene("Scenes/Login");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
