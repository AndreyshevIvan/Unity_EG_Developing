using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestApplication : MonoBehaviour
{
    public Chat m_chat;
    public UIManager m_UIManager;
    public GameObject m_rooms;

    public ChatIcon m_iconFirst;

    string m_chatName = "testChat";
    bool m_isInit = false;

    private void Start()
    {
        m_isInit = true;
        m_chat = new Chat(m_UIManager, m_chatName);
        m_chat.SetIcon(m_iconFirst);
    }

    void LateUpdate()
    {
        m_chat.Update(Time.deltaTime);
    }

    public void OpenRooms(bool isOpen)
    {
        m_rooms.SetActive(isOpen);
    }

    private void OnDestroy()
    {
        DataManager.SaveHistory(m_chat.GetHistory());
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
