using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestApplication : MonoBehaviour
{
    public Chat m_chat;
    public UIManager m_UIManager;
    public ChatIcon m_iconFirst;

    string m_chatName = "testChat";
    bool m_isFastMode = false;

    private void Start()
    {
        m_chat = new Chat(m_UIManager, m_chatName);
        m_chat.SetIcon(m_iconFirst);
        m_UIManager.OpenRooms(true);
    }

    public void SetFastMode()
    {
        m_isFastMode = !m_isFastMode;
        m_chat.SetColdownIgnore(m_isFastMode);
        m_UIManager.SetColdownIgnore(m_isFastMode);
    }

    void LateUpdate()
    {
        m_chat.Update(Time.deltaTime);
    }

    void OnDestroy()
    {
        if (m_chat != null)
        {
            History history = m_chat.GetHistory();
            DataManager.SaveHistory(history);
        }
    }

    public void RestartGame()
    {
        m_chat.GetHistory().Reset();
        DataManager.ResetAll(m_chatName);
        SceneManager.LoadScene("Scenes/Login");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
