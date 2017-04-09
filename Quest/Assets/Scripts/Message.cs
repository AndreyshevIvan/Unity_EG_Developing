using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public MessageSide m_side;

    Text m_text;
    LayoutElement m_layoutElement;
    RectTransform m_transform;

    const float MAX_RELATIVE_WIDTH = 0.75f;
    const float RELATIVE_FONT_SIZE = 0.025f;
    const float RELATIVE_OFFSET_LEFT = 0.05f;

    void Awake()
    {
        m_text = GetComponentInChildren<Text>();
        m_layoutElement = GetComponentInChildren<LayoutElement>();
        m_transform = GetComponent<RectTransform>();
    }

    public Vector3 GetDownLeftPosition()
    {
        return transform.position;
    }
    public float GetHeight()
    {
        return m_transform.rect.size.y;
    }

    void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_transform);

        Vector2 newSize = m_transform.rect.size;

        if (newSize.x > Screen.width * MAX_RELATIVE_WIDTH)
        {
            m_layoutElement.preferredWidth = Screen.width * MAX_RELATIVE_WIDTH;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(m_transform);
    } 
    public void UpdateTransform(Transform parent)
    {
        transform.SetParent(parent);

        Vector2 parentSize = parent.GetComponent<RectTransform>().rect.size;
        Vector3 position = transform.localPosition;
        float sideOffset = parentSize.x * RELATIVE_OFFSET_LEFT;
        float positionX = sideOffset;
        positionX = (m_side == MessageSide.RIGHT) ? parentSize.x - sideOffset : positionX;

        transform.localPosition = new Vector3(positionX, 0, position.z);
    }
    public void SetText(string text)
    {
        m_text.text = text;
        m_text.fontSize = (int)(Screen.height * RELATIVE_FONT_SIZE);
        UpdateLayout();
    }
}