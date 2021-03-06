﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public GameObject m_soundOnImg;
    public GameObject m_soundOffImg;

    DataController m_data;

    bool m_isSoundOn;

    private void Awake()
    {
        m_data = new DataController();
        m_isSoundOn = m_data.IsSoundActive();

        m_soundOnImg.SetActive(m_isSoundOn);
        m_soundOffImg.SetActive(!m_isSoundOn);

        AudioListener.pause = !m_isSoundOn;
    }

    public void SitchSound()
    {
        m_isSoundOn = !m_isSoundOn;

        AudioListener.pause = !m_isSoundOn;

        m_soundOnImg.SetActive(m_isSoundOn);
        m_soundOffImg.SetActive(!m_isSoundOn);

        m_data.SetSoundActive(m_isSoundOn);
    }

}