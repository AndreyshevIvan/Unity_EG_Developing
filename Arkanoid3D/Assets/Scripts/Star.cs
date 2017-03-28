using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    Image m_image;
    Animation m_animation;
    bool m_isUnlock;

    private void Awake()
    {
        m_image = GetComponent<Image>();
        m_animation = GetComponent<Animation>();
    }

    public void Lock(Color lockedColor)
    {
        m_image.color = lockedColor;
    }

    public void Unlock(Color unclockedColor)
    {
        m_image.color = unclockedColor;
        m_animation.Play();
        m_isUnlock = true;
    }

    public bool IsUnlock(bool isCheckAnim = false)
    {
        bool animEnd = (isCheckAnim) ? !m_animation.isPlaying : true;

        return m_isUnlock && animEnd;
    }
}
