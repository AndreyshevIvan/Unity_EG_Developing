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
    public GameObject m_newMsgAnnouncer;

    RectTransform m_transform;

    const float RELATIVE_HEIGHT = 0.2f;

    public void Init(string name)
    {
        m_title.text = name;
    }

    public void SetNewMsgAnnounce(bool isActive)
    {
        m_newMsgAnnouncer.SetActive(isActive);
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
        if (onClickEvent != null)
        {
            onClickEvent();
        }
    }
}
