using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Spawner : MonoBehaviour
{
    public GameObject m_block;
    public string[] m_mapsPath;
    private int m_blocksInLine = 20;

    StreamReader m_reader;

    Vector3 m_floorScale;
    Vector3 m_floorPosition;
    Vector3 m_blockScale;
    float m_height;
    float m_posZFactor = 0.95f;

    public void Init(Vector3 floorScale, Vector3 floorPosition, float blocksHeight)
    {
        m_floorScale = floorScale;
        m_floorPosition = floorPosition;
        m_height = blocksHeight;
        m_blockScale = m_block.transform.localScale;
    }

    public void SpawnLevel()
    {
        SetStartPosition();

        m_reader = new StreamReader(m_mapsPath[0]);

        string line = m_reader.ReadLine();
        while (line != null)
        {
            SpawnLine(line);
            line = m_reader.ReadLine();
        }

        m_reader.Close();
    }

    void SpawnLine(string line)
    {
        Debug.Log(line.Length);

        for (int i = 0; i < m_blocksInLine; i++)
        {
            if (line[i] == '#')
            {
                GameObject block = Instantiate(m_block, gameObject.transform.position, Quaternion.identity);
            }
            gameObject.transform.position += new Vector3(m_blockScale.x, 0, 0);
        }

        gameObject.transform.position -= new Vector3(m_blockScale.x * line.Length, 0, m_blockScale.z);
    }

    void SetStartPosition()
    {
        float posX = m_floorPosition.x - ((m_blocksInLine - 1) * m_blockScale.x) / 2;
        float posY = m_height;
        float posZ = (m_floorPosition.z + m_floorScale.z / 2.0f) * m_posZFactor;

        Debug.Log(new Vector3(posX, posY, posZ));

        gameObject.transform.position = new Vector3(posX, posY, posZ);
    }
}
