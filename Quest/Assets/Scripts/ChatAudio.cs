using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatAudio : MonoBehaviour
{
    AudioSource m_source;

    public AudioClip m_newMessage;
    public AudioClip m_messageAnnounce;

    void Awake()
    {
        m_source = GetComponent<AudioSource>();
    }

    public void PlayNewMsgSound()
    {
        PlaySound(m_newMessage);
    }
    public void PlayMsgAnnounceSound()
    {
        PlaySound(m_messageAnnounce);
    }
    void PlaySound(AudioClip clip)
    {
        if (m_source != null && clip != null)
        {
            m_source.Stop();
            m_source.clip = clip;
            m_source.Play();
        }
    }
}
