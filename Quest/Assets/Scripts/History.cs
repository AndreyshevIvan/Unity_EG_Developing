using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class Pair<T, U>
{
    public Pair()
    {
    }

    public Pair(T first, U second)
    {
        this.first = first;
        this.second = second;
    }

    public T first { get; set; }
    public U second { get; set; }
};

[System.Serializable]
public class History
{
    public History(string chatName)
    {
        m_replics = new List<Pair<string, string>>();
        m_replics.Clear();
        m_name = chatName;
    }

    List<Pair<string, string>> m_replics;
    int m_state;
    string m_name;

    public void AddReplica(Pair<string, string> replica, int state)
    {
        m_replics.Add(replica);
        m_state = state;
    }
    public string GetName()
    {
        return m_name;
    }
    public int GetState()
    {
        return m_state;
    }
}
