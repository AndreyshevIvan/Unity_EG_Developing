using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatIcon : MonoBehaviour
{
    public delegate void OnIconClick();
    public OnIconClick onClickEvent;

    public Text m_title;
    public Text m_lastMessage;
    public Image m_userIcon;

    RectTransform m_transform;

    const float RELATIVE_HEIGHT = 0.2f;

    void Awake()
    {

    }

    public void Init(string name, Image icon)
    {
        m_title.text = name;
        m_userIcon = icon;
    }

    public void SetLastMessage(string message)
    {
        string lastMsg = "";

        if (message != "")
        {
            for (int i = 0; i < 20; i++)
            {
                if (i < message.Length - 1)
                {
                    lastMsg += message[i];
                }
            }
        }

        m_lastMessage.text = lastMsg;
    }

    public void OnClickEvent()
    {
        onClickEvent();
    }
}
