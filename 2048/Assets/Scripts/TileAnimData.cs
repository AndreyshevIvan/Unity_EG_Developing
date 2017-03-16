using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TileAnimData
{
    public TileAnimData(int first, int second, int third)
    {
        m_first = first;
        m_second = second;
        m_third = third;
    }

    int m_first;
    int m_second;
    int m_third;

    public int first
    {
        get
        {
            return m_first;
        }

        set
        {
            m_first = value;
        }
    }

    public int second
    {
        get
        {
            return m_second;
        }

        set
        {
            m_second = value;
        }
    }

    public int third
    {
        get
        {
            return m_third;
        }

        set
        {
            m_third = value;
        }
    }
}