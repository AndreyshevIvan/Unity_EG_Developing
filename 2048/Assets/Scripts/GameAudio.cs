using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioSource m_source;
    public AudioSource m_backSource;

    public AudioClip m_music;

    public AudioClip m_buttonClick;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject.transform);
    }

    public void StartBackgroundMusic()
    {
        m_backSource.Stop();
        m_backSource.clip = m_music;
        m_backSource.loop = true;
        m_backSource.Play();
    }

    public void ButtonClick()
    {
        m_source.Stop();
        m_source.clip = m_buttonClick;
        m_source.Play();
    }
}
