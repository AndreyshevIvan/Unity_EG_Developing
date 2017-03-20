using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldEventsHandler : MonoBehaviour, IDragHandler, IBeginDragHandler
{

    FieldController m_fieldController;
    FieldViewer m_fieldViewer;
    FieldAudio m_audio;

    private void Awake()
    {
        m_fieldController = GetComponent<FieldController>();
        m_fieldViewer = GetComponent<FieldViewer>();
        m_audio = GetComponent<FieldAudio>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!m_fieldViewer.IsMoveAnimationWork())
        {
            float verticalDelta = Mathf.Abs(eventData.delta.y);
            float horizontalDelta = Mathf.Abs(eventData.delta.x);

            if (horizontalDelta > verticalDelta)
            {
                if (eventData.delta.x > 0) RightTurn();
                else if (eventData.delta.x < 0) LeftTurn();
            }
            else
            {
                if (eventData.delta.y > 0) UpTurn();
                else if (eventData.delta.y < 0) DownTurn();
            }
        }
    }

    void RightTurn()
    {
        if (m_fieldController.RightTurn())
        {
            m_audio.Swipe();
            byte[,] animMap = m_fieldController.GetCurrentAnimMap();
            m_fieldViewer.AnimateRightTurn(animMap);
        }
    }
    void LeftTurn()
    {
        if (m_fieldController.LeftTurn())
        {
            m_audio.Swipe();
            byte[,] animMap = m_fieldController.GetCurrentAnimMap();
            m_fieldViewer.AnimateLeftTurn(animMap);
        }
    }
    void UpTurn()
    {
        if (m_fieldController.UpTurn())
        {
            m_audio.Swipe();
            byte[,] animMap = m_fieldController.GetCurrentAnimMap();
            m_fieldViewer.AnimateUpTurn(animMap);
        }
    }
    void DownTurn()
    {
        if (m_fieldController.DownTurn())
        {
            m_audio.Swipe();
            byte[,] animMap = m_fieldController.GetCurrentAnimMap();
            m_fieldViewer.AnimateDownTurn(animMap);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

}
