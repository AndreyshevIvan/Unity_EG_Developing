using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewIcon : MonoBehaviour
{
    Image m_image;
    Animation m_animation;

    const string ANIM_ON = "PreviewON";
    const string ANIM_OFF = "PreviewOFF";

    void Awake()
    {
        m_image = GetComponent<Image>();
        m_animation = GetComponent<Animation>();
    }

    public void SetIconActive(bool isActive)
    {
        string animName = (isActive) ? ANIM_ON : ANIM_OFF;
        m_animation.Play(animName);
    }

    public bool IsReadyToSwap()
    {
        return !m_animation.isPlaying;
    }
}
