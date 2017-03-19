using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public GameObject m_field;

    byte m_mapIndex = 0;

    const byte m_normalMapSize = 4;
    const byte m_largeMapSize = 5;
    const byte m_extraLargeMapSize = 6;

    float m_width;

    public Field GetField(int mapIndex)
    {
        m_mapIndex = (byte)mapIndex;
        byte mapSize = m_normalMapSize;

        switch (m_mapIndex)
        {
            case 0:
                mapSize = m_normalMapSize;
                break;
            case 1:
                mapSize = m_largeMapSize;
                break;
            case 2:
                mapSize = m_extraLargeMapSize;
                break;
        }

        GameObject map = Instantiate(m_field, transform.position, Quaternion.identity);
        map.transform.SetParent(transform);

        Vector2 size = GetComponent<RectTransform>().rect.size;
        m_width = (size.x < size.y) ? size.x : size.y;

        Vector3 positionOffset = new Vector3(0, m_width / 2, 0);
        map.transform.localPosition += positionOffset;

        Field field = map.GetComponentInChildren<Field>();
        field.Create(mapSize, m_width);

        return field;
    }
}
