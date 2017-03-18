using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StatsPair
{
    string m_name;
    uint m_score;

    StatsPair(string name, uint score)
    {
        m_name = name;
        m_score = score;
    }

    public string name
    {
        get
        {
            return m_name;
        }
    }

    public uint score
    {
        get
        {
            return m_score;
        }
    }
}