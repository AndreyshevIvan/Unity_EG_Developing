using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat
{
    public Chat(IMessagesBox messageBox, ChatIcon icon, ChatAudio audio, string name)
    {
        m_history = DataManager.LoadHistory(name);
        m_chatState = m_history.GetState();

        m_name = name;
        m_replics = new ReplicaController(m_name);

        m_messageBox = messageBox;
        m_messageBox.LoadFromHistory(m_history);
        m_messageBox.AddPlayerTurnEvent(SetState);

        m_icon = icon;
        m_icon.onClickEvent += Activate;
        m_icon.InitChat(m_name);
        m_icon.SetVisible(true);

        m_audio = audio;

        InitUsers();
    }

    public delegate void OnNewKeyEvent(string key);
    public OnNewKeyEvent onKeyEvent;

    ChatIcon m_icon;

    ReplicaController m_replics;
    IMessagesBox m_messageBox;
    History m_history;
    ChatAudio m_audio;

    AIPlayer m_computer;
    Player m_player;
    User m_currentUser;
    List<User> m_users;

    int m_userNumber = 0;
    int m_chatState;
    string m_name;
    bool m_isChatActive = false;
    bool m_isNewMsgExist = false;
    bool m_isFirstInitComplete = false;

    void InitUsers()
    {
        string playerName = DataManager.playerName;

        m_player = new Player(m_messageBox, playerName);
        m_computer = new AIPlayer(m_messageBox, m_name);

        m_player.InitReplicsManager(m_replics);
        m_computer.InitReplicsManager(m_replics);

        m_player.onSendMessage += m_icon.SetLastMessage;
        m_computer.onSendMessage += m_icon.SetLastMessage;

        m_player.SetHistory(m_history);
        m_computer.SetHistory(m_history);

        m_users = new List<User>();

        m_users.Add(m_computer);
        m_users.Add(m_player);

        m_computer.turnEvent += SetNewMsg;
        m_currentUser = m_computer;
        m_currentUser.SetNewTurn(m_chatState);
    }
    void FirstInit()
    {
        if (!m_isFirstInitComplete)
        {
            m_isFirstInitComplete = true;

            UpdateKeys();
        }
    }

    public void Update(float delta)
    {
        FirstInit();

        if (m_currentUser.SendMessage(delta))
        {
            SwitchUser();
            m_currentUser.SetNewTurn(m_chatState);
        }

        m_icon.SetNewMsgAnnounce(m_isNewMsgExist);
    }
    public void UpdateKeys()
    {
        List<string> keys = m_replics.GetStateKey(m_chatState);

        foreach (string key in keys)
        {
            onKeyEvent(key);
        }
    }

    public void SetColdownIgnore(bool isIgnore)
    {
        m_computer.SetIgnoreColdown(isIgnore);
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
    void SetNewMsg()
    {
        if (!m_isChatActive)
        {
            m_isNewMsgExist = true;
            m_audio.PlayNewMsgSound();
        }
        else
        {
            m_audio.PlayMsgAnnounceSound();
        }
    }
    void SetState(int state, string message)
    {
        m_chatState = state;
        UpdateKeys();
    }

    public void Activate()
    {
        m_isChatActive = true;
        m_isNewMsgExist = false;
        m_messageBox.SetVisible(true);
        m_icon.SetActive(m_name);
    }
    public void Diactivate()
    {
        m_isChatActive = false;
        m_messageBox.SetVisible(false);
    }

    public History GetHistory()
    {
        return m_history;
    }
}