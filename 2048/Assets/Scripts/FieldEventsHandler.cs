using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldEventsHandler : MonoBehaviour, IDragHandler, IBeginDragHandler
{

    FieldController m_field;
    FieldViewer m_fieldViewer;

    private void Awake()
    {
        m_field = GetComponent<FieldController>();
        m_fieldViewer = GetComponent<FieldViewer>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (m_fieldViewer.IsAnimationsEnded())
        {
            float verticalDelta = Mathf.Abs(eventData.delta.y);
            float horizontalDelta = Mathf.Abs(eventData.delta.x);

            if (horizontalDelta > verticalDelta)
            {
                if (eventData.delta.x > 0)
                {
                    m_field.RightTurn();
                    m_fieldViewer.AnimateRightTurn();
                }
                else
                {
                    m_field.LeftTurn();
                    m_fieldViewer.AnimateLeftTurn();
                }
            }
            else
            {
                if (eventData.delta.y > 0)
                {
                    m_field.UpTurn();
                    m_fieldViewer.AnimateUpTurn();
                }
                else
                {
                    m_field.DownTurn();
                    m_fieldViewer.AnimateDownTurn();
                }
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {

    }

}
