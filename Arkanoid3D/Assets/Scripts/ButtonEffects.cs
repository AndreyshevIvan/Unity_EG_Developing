using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Effect
{
    public Effect(GameObject button)
    {
        m_offset = TEXT_OFFSET * Screen.width;
        m_button = button;
        m_buttonText = button.GetComponentInChildren<Text>();
        m_textTransform = m_buttonText.GetComponent<RectTransform>();
        m_startPosition = m_textTransform.localPosition;
    }

    protected GameObject m_button;
    protected Text m_buttonText;
    protected RectTransform m_textTransform;
    protected float m_offset = 0;
    protected Vector3 m_startPosition;

    protected const float TEXT_OFFSET = 0.03f; // (offsetInPixels / Screen.width)
    protected const float OFFSET_SPEED = 400;

    public void Reset()
    {
        m_textTransform.localPosition = m_startPosition;
    }
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
        Vector3 position = m_textTransform.localPosition;

        if (position.x < m_startPosition.x + m_offset)
        {
            float offset = OFFSET_SPEED * delta;
            m_textTransform.localPosition += new Vector3(offset, 0, 0);
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
        Vector3 position = m_textTransform.localPosition;

        if (position.x > m_startPosition.x)
        {
            float offset = OFFSET_SPEED * delta;
            m_textTransform.localPosition -= new Vector3(offset, 0, 0);
        }
    }
}

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button m_button;

    EffectOn m_strokeOn;
    EffectOff m_strokeOff;
    Effect m_strokeEffect;

    public MenuPreview m_previewController;
    public int m_iconIndex; // from 0

    void Awake()
    {
        m_button = GetComponent<Button>();
        m_strokeOn = new EffectOn(gameObject);
        m_strokeOff = new EffectOff(gameObject);
        m_strokeEffect = m_strokeOff;
    }

    private void OnEnable()
    {
        m_strokeEffect = m_strokeOff;
        m_strokeEffect.Reset();
    }

    public void SetInteractable(bool isInteractable)
    {
        m_button.interactable = isInteractable;
    }
    public void SetArtificialActive(bool isArtificial)
    {
        if (isArtificial)
        {
            m_strokeEffect = m_strokeOn;
        }
        else
        {
            m_strokeEffect = m_strokeOff;
        }
    }

    void FixedUpdate()
    {
        m_strokeEffect.Update(Time.deltaTime);
        if (IsInteractable() && m_strokeEffect == m_strokeOn)
        {
            SetIcon();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsInteractable())
        {
            m_strokeEffect = m_strokeOn;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsInteractable())
        {
            m_strokeEffect = m_strokeOff;
        }
    }

    void SetIcon()
    {
        if (m_previewController != null)
        {
            m_previewController.SetIcon(m_iconIndex);
        }
    }

    bool IsInteractable()
    {
        return m_button.interactable;
    }
}
