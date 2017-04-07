using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public User(IMessagesBox messagesBox)
    {
        m_messageBox = messagesBox;
    }

    string m_name;
    string[] m_replica;
    float m_coldown;

    IMessagesBox m_messageBox;
    int m_state;

    const float ENEMY_COLDOWN = 0.02f;

    public void SetTurn(int turnState)
    {
        m_state = turnState;
    }
    public virtual bool SendMessage()
    {
        return true;
    }
}