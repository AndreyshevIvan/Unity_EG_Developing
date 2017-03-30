using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusPlate : MonoBehaviour
{
    public Text m_name;
    public Text m_value;

    public Animation m_anim;

    RectTransform m_transform;

    float m_oneOffset = 0;
    float m_moveing = 0;
    float m_duration = 0;
    bool m_isDurationInValue = true;

    const string EXIT_ANIM = "BonusPlateExit";
    const string ENTER_ANIM = "BonusPlateEnter";
    const float MIN_MOVEMENT = 0.3f;
    const float MOVEMENT_SPEED = 5;

    void Awake()
    {
        m_transform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }
    public void Init(string name, Vector2 size)
    {
        SetName(name);
        SetSize(size);
        SetPosition(new Vector3(0, 0, 0));
        m_oneOffset = m_transform.rect.size.y;
    }
    void Start()
    {

    }

    void FixedUpdate()
    {
        UpdatePosition();
        UpdateValue();
        UpdateTimer();
        UpdateLife();
    }
    void UpdatePosition()
    {
        if (m_moveing > 0)
        {
            float movement = Time.deltaTime * m_moveing * MOVEMENT_SPEED;
            if (movement < MIN_MOVEMENT)
            {
                movement = MIN_MOVEMENT;
            }

            m_moveing -= movement;
            m_transform.position += new Vector3(0, movement, 0);
        }
    }
    void UpdateTimer()
    {
        if (IsActive())
        {
            m_duration -= Time.deltaTime;
        }
    }
    void UpdateValue()
    {
        m_value.text = "";

        if (m_isDurationInValue)
        {
            m_value.text = ((int)(m_duration + 1)).ToString();
        }
    }
    void UpdateLife()
    {

    }

    public void Move(byte moveCount)
    {
        m_moveing += m_oneOffset * moveCount;
    }
    public void AddDuration(float addDuration)
    {
        m_duration += addDuration;
    }

    public void Enter()
    {
        gameObject.SetActive(true);
        m_moveing = 0;
    }
    public void Exit()
    {
        gameObject.SetActive(false);
        m_moveing = 0;
    }

    void SetSize(Vector2 size)
    {
        m_transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        m_transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
    }
    public void SetPosition(Vector3 position)
    {
        m_transform.localPosition = position;
    }
    void SetName(string name)
    {
        m_name.text = name;
        gameObject.name = name;
    }
    public void SetValue(string value)
    {
        m_value.text = value;
    }
    public void DurationInValue(bool isEnable)
    {
        m_isDurationInValue = isEnable;
    }

    public int GetTimeInSeconds()
    {
        return (int)(m_duration + 1);
    }

    public bool IsActive()
    {
        return (m_duration > 0);
    }
}
