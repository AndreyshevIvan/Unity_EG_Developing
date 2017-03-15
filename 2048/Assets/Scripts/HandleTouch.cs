using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeType
{
    None,
    Up,
    Down,
    Left,
    Right,
}

public class HandleTouch : MonoBehaviour
{

    Vector2 m_startPos;
    Vector2 m_direction;
    bool m_directionChosen;
    SwipeType m_swipeType = SwipeType.None;

    float m_handleColdown = 0.16f;
    float m_coldown = 0;

    void Update()
    {
        m_coldown += Time.deltaTime;

        if (Input.touchCount > 0 && m_coldown > m_handleColdown)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    m_startPos = touch.position;
                    m_directionChosen = false;
                    break;

                case TouchPhase.Moved:
                    m_direction = touch.position - m_startPos;
                    break;

                case TouchPhase.Ended:
                    m_directionChosen = true;
                    break;
            }
        }

        if (m_directionChosen)
        {
            m_coldown = 0;
            m_directionChosen = false;
            DetermineTouchDirection();
        }
    }

    void DetermineTouchDirection()
    {
        float absVertical = Mathf.Abs(m_direction.x);
        float absHorizontal = Mathf.Abs(m_direction.y);

        if (absVertical < absHorizontal) // Horizontal swipe
        {
            if (m_direction.x > 0)
            {
                m_swipeType = SwipeType.Up;
            }
            else
            {
                m_swipeType = SwipeType.Down;
            }
        }
        else  // Vertical swipe
        {
            if (m_direction.y > 0)
            {
                m_swipeType = SwipeType.Right;
            }
            else
            {
                m_swipeType = SwipeType.Left;
            }
        }
    }

    public SwipeType GetSwipeType()
    {
        return m_swipeType;
    }

    public void Reset()
    {
        m_swipeType = SwipeType.None;
    }
}