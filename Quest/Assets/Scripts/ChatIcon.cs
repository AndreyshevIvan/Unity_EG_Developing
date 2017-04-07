using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatIcon : MonoBehaviour
{
    public Text m_title;
    public Image m_userIcon;

    public void Init(string name, Image icon)
    {
        m_title.text = name;
        m_userIcon = icon;
    }

    public void SetNewMessage()
    {

    }
}
