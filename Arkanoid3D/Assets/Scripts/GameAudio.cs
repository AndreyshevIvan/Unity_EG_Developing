using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    AudioSource m_soundSource;
    AudioSource m_musicSource;

    public AudioClip m_pointsAdd;
    public AudioClip m_timeAdd;

    public AudioClip m_platformBounce;
    public AudioClip m_blockBounce;
    public AudioClip m_blockDeath;

    public AudioClip m_bonusUse;
    public AudioClip m_wallStart;
    public AudioClip m_wallEnd;

    public AudioClip m_menuMusic;
    public AudioClip m_gameMusic;
    public AudioClip m_pauseMusic;

    private void Awake()
    {
        AudioSource[] sourses = GetComponents<AudioSource>();
        m_soundSource = sourses[0];
        m_musicSource = sourses[1];
    }

    void Start()
    {

    }

    void FixedUpdate()
    {

    }


}
