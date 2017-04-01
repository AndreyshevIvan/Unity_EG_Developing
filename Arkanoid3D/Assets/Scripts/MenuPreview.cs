using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPreview : MonoBehaviour
{
    public PreviewIcon[] m_icons;
    PreviewIcon m_currIcon;

    const float SWITCH_DELAY = 1f;

    private void Awake()
    {
        m_currIcon = m_icons[0];
    }

    private void Start()
    {
        for (int i = 0; i < m_icons.Length; i++)
        {
            m_icons[i].SetIconActive(false);
        }

        m_currIcon.SetIconActive(true);
    }

    public void SetIcon(int index)
    {
        if (m_currIcon != m_icons[index])
        {
            m_currIcon.SetIconActive(false);
            m_currIcon = m_icons[index];
            m_currIcon.SetIconActive(true);
        }
    }

    private void FixedUpdate()
    {

    }
}
