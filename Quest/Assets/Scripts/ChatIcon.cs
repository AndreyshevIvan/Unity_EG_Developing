using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatIcon : MonoBehaviour
{
    public delegate void OnIconClick();
    public OnIconClick onClickEvent;

    IChatUI m_chatsUI;
    public Text m_title;
    public Text m_lastMessage;
    public Image m_userIcon;
    public GameObject m_newMsgAnnouncer;

    RectTransform m_transform;
    Vector3 m_startPosition;

    const float INVISIBLE_POSITION = 10000;
    const float RELATIVE_HEIGHT = 0.2f;
    const int LAST_MSG_CHARS_COUNT = 20;

    void Awake()
    {
        m_transform = GetComponent<RectTransform>();
        m_startPosition = m_transform.position;
        SetVisible(false);
    }
    public void InitUI(IChatUI chatsUI)
    {
        m_chatsUI = chatsUI;
    } 
    public void InitChat(string name)
    {
        m_title.text = name;
    }

    public void SetVisible(bool isVisible)
    {
        if (isVisible)
        {
            m_transform.position = new Vector2(m_startPosition.x, m_transform.position.y);
        }
        else
        {

            m_transform.position = new Vector2(INVISIBLE_POSITION, m_transform.position.y);
        }
    }
    public void SetNewMsgAnnounce(bool isActive)
    {
        m_newMsgAnnouncer.SetActive(isActive);
        m_chatsUI.NewMessageAnnounce(isActive);
    }
    public void SetLastMessage(string hostName, string message)
    {
        string lastMsg = "";

        if (message != "")
        {
            lastMsg += hostName + ": ";

            if (message.Length < LAST_MSG_CHARS_COUNT)
            {
                lastMsg += message;
            }
            else
            {
                for (int i = 0; i < LAST_MSG_CHARS_COUNT; i++)
                {
                    lastMsg += message[i];
                }

                lastMsg += "...";
            }
        }

        m_lastMessage.text = lastMsg;
    }
    public void SetActive(string chatName)
    {
        m_chatsUI.SetChatName(chatName);
        m_chatsUI.SetChatIcon(m_userIcon.sprite);
    }

    public void OnClickEvent()
    {
        if (onClickEvent != null)
        {
            onClickEvent();
        }
    }
}
