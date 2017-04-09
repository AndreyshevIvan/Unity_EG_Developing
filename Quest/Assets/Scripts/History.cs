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
        m_replics = new List<Pair<MessageSide, string>>();
        m_replics.Clear();
        m_name = chatName;
    }

    List<Pair<MessageSide, string>> m_replics;
    int m_state;
    string m_name;

    public void AddPlayerReplica(string message, int state)
    {
        Pair<MessageSide, string> replica = new Pair<MessageSide, string>(MessageSide.RIGHT, message);
        m_replics.Add(replica);
        m_state = state;
    }
    public void AddComputerReplica(List<UserReplica> computerReplics, int state)
    {
        foreach (UserReplica aiReplica in computerReplics)
        {
            string message = aiReplica.toSend;
            Pair<MessageSide, string> replica = new Pair<MessageSide, string>(MessageSide.LEFT, message);
            m_replics.Add(replica);
        }

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
    public List<Pair<MessageSide, string>> GetReplics()
    {
        return m_replics;
    }

    public void Reset()
    {
        m_state = 0;
        m_replics.Clear();
    }
}
