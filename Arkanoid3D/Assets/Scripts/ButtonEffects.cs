using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Effect
{
    public Effect(GameObject button)
    {
        m_button = button;
        m_buttonText = button.GetComponentInChildren<Text>();
        m_textTransform = m_buttonText.GetComponent<RectTransform>();
        m_startSize = m_textTransform.rect.size;
    }

    protected GameObject m_button;
    protected Text m_buttonText;
    protected RectTransform m_textTransform;
    protected float m_offset = 0;
    protected Vector2 m_startSize;

    protected const float TEXT_OFFSET = 80;
    protected const float OFFSET_SPEED = 500;

    public virtual void Update(float delta) { }
}

public class EffectOn : Effect
{
    public EffectOn(GameObject button)
        : base(button)
    {
    }

    public override void Update(float delta)
    {
        Vector2 size = m_textTransform.rect.size;

        if (size.x < m_startSize.x + TEXT_OFFSET)
        {
            float addWidth = OFFSET_SPEED * delta;
            m_textTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x + addWidth);
        }
    }
}

public class EffectOff : Effect
{
    public EffectOff(GameObject button)
        : base(button)
    {
    }

    public override void Update(float delta)
    {
        Vector2 size = m_textTransform.rect.size;

        if (size.x > m_startSize.x)
        {
            float addWidth = OFFSET_SPEED * delta;
            m_textTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x - addWidth);
        }
    }
}

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    EffectOn m_strokeOn;
    EffectOff m_strokeOff;
    Effect m_strokeEffect;

    public GameObject m_stroke;

    private void Awake()
    {
        m_strokeOn = new EffectOn(gameObject);
        m_strokeOff = new EffectOff(gameObject);
        m_strokeEffect = m_strokeOff;
    }

    private void FixedUpdate()
    {
        m_strokeEffect.Update(Time.deltaTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_strokeEffect = m_strokeOn;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_strokeEffect = m_strokeOff;
    }
}
