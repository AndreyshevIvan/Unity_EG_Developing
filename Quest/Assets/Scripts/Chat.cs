using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat
{
    public Chat(IMessagesBox messageBox, string name)
    {
        m_chatState = 0;
        m_name = name;
        m_messageBox = messageBox;
        m_replics = new ReplicaController(m_name);
        messageBox.AddPlayerTurnEvent(SetState);

        InitUsers();
        //m_icon.Init(m_name, m_titleImage);
    }

    public ChatIcon m_icon;

    ReplicaController m_replics;
    IMessagesBox m_messageBox;
    History m_history;

    AIPlayer m_computer;
    Player m_player;
    User m_currentUser;
    List<User> m_users;

    int m_userNumber = 0;
    int m_chatState;
    string m_name;

    void InitUsers()
    {
        string playerName = DataManager.playerName;

        m_player = new Player(m_messageBox, playerName);
        m_computer = new AIPlayer(m_messageBox, m_name);

        m_player.InitReplicsManager(m_replics);
        m_computer.InitReplicsManager(m_replics);

        m_users = new List<User>();

        m_users.Add(m_computer);
        m_users.Add(m_player);

        m_currentUser = m_computer;
        m_currentUser.SetNewTurn(m_chatState);
    }

    public void Update(float delta)
    {
        if (m_currentUser.SendMessage(delta))
        {
            SwitchUser();
            m_currentUser.SetNewTurn(m_chatState);
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
    void SetState(int state)
    {
        m_chatState = state;
    }
}