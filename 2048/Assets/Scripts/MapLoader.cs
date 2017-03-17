using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public GameObject m_fieldParent;
    public GameObject m_field;

    byte m_mapIndex = 0;

    public Field GetField(int mapIndex)
    {
        m_mapIndex = (byte)mapIndex;
        byte mapSize = 6;

        switch (m_mapIndex)
        {
            case 0:

                break;
            case 1:

                break;
        }

        GameObject map = Instantiate(m_field, transform.position, Quaternion.identity);
        map.transform.SetParent(m_fieldParent.transform);

        Field field = map.GetComponentInChildren<Field>();
        field.Create(mapSize);

        return field;
    }
}
