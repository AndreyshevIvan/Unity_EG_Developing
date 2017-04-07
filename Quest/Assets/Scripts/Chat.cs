using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat
{
    public string m_name;
    public Image m_titleImage;

    public ChatIcon m_icon;

    History m_history;
    User m_computer;
    Player m_player;

    User m_currentUser;
    List<User> m_users;
    int m_userNumber = 0;

    int m_chatState;

    private void Awake()
    {
        m_chatState = 0;
    }
    public void Init(IMessagesBox messageBox)
    {
        InitUsers(messageBox);
        m_icon.Init(m_name, m_titleImage);
        messageBox.SetHistory(m_name);
    }
    void InitUsers(IMessagesBox messageBox)
    {
        m_player = new Player(messageBox);
        m_computer = new User(messageBox);

        m_users = new List<User>();

        m_users.Add(m_computer);
        m_users.Add(m_player);

        m_userNumber = 0;
    }

    public void Update(float delta)
    {
        if (m_currentUser.SendMessage())
        {
            SwitchUser();
            m_currentUser.SetTurn(m_chatState);
        }
    }

    void SwitchUser()
    {
        m_userNumber++;

        if (m_userNumber == m_users.Count)
        {
            m_userNumber = 0;
        }

        m_currentUser = m_users[m_userNumber];
    }
}