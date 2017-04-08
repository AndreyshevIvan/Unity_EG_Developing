using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    Text m_text;
    LayoutElement m_layoutElement;
    RectTransform m_transform;

    Vector3 m_downLeftPosition;

    const float MAX_RELATIVE_WIDTH = 0.75f;
    const float RELATIVE_FONT_SIZE = 0.035f;
    const float RELATIVE_OFFSET_LEFT = 0.05f;

    void Awake()
    {
        m_text = GetComponentInChildren<Text>();
        m_layoutElement = GetComponentInChildren<LayoutElement>();
        m_transform = GetComponent<RectTransform>();
    }

    public Vector3 GetDownLeftPosition()
    {
        return m_downLeftPosition;
    }
    public float GetHeight()
    {
        return m_transform.rect.size.y;
    }

    public void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_transform);

        Vector2 newSize = m_transform.rect.size;

        if (newSize.x > Screen.width)
        {
            m_layoutElement.preferredWidth = Screen.width * MAX_RELATIVE_WIDTH;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(m_transform);
    } 
    public void SetTransformProperty(Transform parent, Vector3 lastPosition, MessageSide size)
    {
        transform.SetParent(parent);

        Vector2 parentSize = parent.GetComponent<RectTransform>().rect.size;
        Vector3 position = transform.localPosition;
        float sideOffset = parentSize.x * RELATIVE_OFFSET_LEFT;
        float positionX = sideOffset;
        positionX = (size == MessageSide.RIGHT) ? parentSize.x - sideOffset : positionX;

        transform.localPosition = new Vector3(positionX, 0, position.z);

        m_downLeftPosition = transform.position;
    }
    public void SetText(string text)
    {
        m_text.text = text;
        m_text.fontSize = (int)(Screen.height * RELATIVE_FONT_SIZE);
    }
}