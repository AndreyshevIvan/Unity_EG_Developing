using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldAudio : MonoBehaviour
{

    public AudioSource m_source;
    public AudioClip m_swipe;

    public void Swipe()
    {
        m_source.Stop();
        m_source.clip = m_swipe;
        m_source.Play();
    }

}
