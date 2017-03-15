using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class IntPair
{
    public IntPair(int first, int second)
    {
        m_first = first;
        m_second = second;
    }

    int m_first;
    int m_second;

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
}