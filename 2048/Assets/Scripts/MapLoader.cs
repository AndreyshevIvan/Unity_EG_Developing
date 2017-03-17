using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public GameObject m_fieldParent;

    GameObject m_field;
    public GameObject m_normalField;
    public GameObject m_largeField;

    byte m_mapIndex = 0;

    private void Awake()
    {
        m_field = m_normalField;
    }

    public void Init(int mapIndex)
    {
        m_mapIndex = (byte)mapIndex;

        switch (m_mapIndex)
        {
            case 0:
                m_field = m_normalField;
                break;
            case 1:
                m_field = m_largeField;
                break;
        }

        GameObject field = Instantiate(m_field, transform.position, Quaternion.identity);
        field.transform.SetParent(m_fieldParent.transform);

        m_field = field;
    }

    public FieldController GetFieldController()
    {
        FieldController fieldController = m_field.GetComponent<FieldController>();

        return fieldController;
    }
    public FieldViewer GetFieldViewer()
    {
        FieldViewer fieldViewer = m_field.GetComponent<FieldViewer>();

        return fieldViewer;
    }
}
