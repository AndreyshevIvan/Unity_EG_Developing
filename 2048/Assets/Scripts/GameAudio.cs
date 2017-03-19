using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioSource m_source;
    public AudioSource m_backSource;

    public AudioClip[] m_music;
    public AudioClip m_buttonClick;
    public AudioClip m_resetAll;
    public AudioClip m_gameOver;

    DataController m_data;

    bool m_isSoundOn = false;

    private void Awake()
    {
        m_data = new DataController();
        m_isSoundOn = m_data.IsSoundActive();

        if (m_isSoundOn)
        {
            StartBackgroundMusic();
        }
    }

    private void FixedUpdate()
    {
        if (m_isSoundOn && !m_backSource.isPlaying)
        {

        }
    }

    public void SetSoundActive(bool isSoundOn)
    {
        m_isSoundOn = isSoundOn;

        if (m_isSoundOn)
        {
            StartBackgroundMusic();
        }
        else
        {
            StopBackGroundMusic();
        }
    }
    public void StartBackgroundMusic()
    {
        if (m_isSoundOn && !m_backSource.isPlaying)
        {
            StopBackGroundMusic();
            m_backSource.clip = GetRandomBackgroundMusic();

            if (m_backSource.clip != null)
            {
                m_backSource.Play();
            }
        }
    }
    public void StopBackGroundMusic()
    {
        m_backSource.Stop();
    }

    AudioClip GetRandomBackgroundMusic()
    {
        AudioClip clip = null;
        int musicCount = m_music.Length;

        if (musicCount != 0)
        {
            int randomTrack = Random.Range(0, musicCount);
            clip = m_music[randomTrack];
        }

        return clip;
    }

    public void ButtonClick()
    {
        if (m_isSoundOn)
        {
            m_source.Stop();
            m_source.clip = m_buttonClick;
            m_source.Play();
        }
    }
    public void ResetAllButton()
    {
        if (m_isSoundOn)
        {
            m_source.Stop();
            m_source.clip = m_resetAll;
            m_source.Play();
        }
    }
    public void GameOver()
    {
        if (m_isSoundOn)
        {
            m_source.Stop();
            m_source.clip = m_gameOver;
            m_source.Play();
        }
    }
}
