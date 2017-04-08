using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatIcon : MonoBehaviour
{
    public Text m_title;
    public Image m_userIcon;
    RectTransform m_transform;

    const float RELATIVE_HEIGHT = 0.12f;

    void Awake()
    {
        CalculateSize();
    }
    void CalculateSize()
    {
        m_transform = GetComponent<RectTransform>();
        RectTransform parentTransform = GetComponentInParent<RectTransform>();
        Vector2 parentSize = parentTransform.rect.size;
        m_transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentSize.y * RELATIVE_HEIGHT);
    }

    public void Init(string name, Image icon)
    {
        m_title.text = name;
        m_userIcon = icon;
    }

    public void SetNewMessage()
    {

    }
}
