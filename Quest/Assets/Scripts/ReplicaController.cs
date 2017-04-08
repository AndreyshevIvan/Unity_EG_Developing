using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public struct PlayerReplica
{
    public PlayerReplica(string toButton, string toSend, int nextState)
    {
        this.toButton = toButton;
        this.toSend = toSend;
        this.nextState = nextState;
    }

    public readonly string toButton;
    public readonly string toSend;
    public readonly int nextState;
}

public class ReplicaController : MonoBehaviour
{
    public ReplicaController(string chatName)
    {
        string replics = DataManager.xmlPath + chatName + DataManager.replicsFileType;

        m_xml = new XmlDocument();
        m_xml.Load(replics);
    }

    XmlDocument m_xml;

    const string STATE_PATTERN = "state_";
    const string NEXT_ATRIBUTE = "next";
    const string TO_BUTTON_ATRIBUTE = "toButton";
    const string AI_REPLICA = "computer";
    const string PLAYER_REPLICA = "player";

    public List<string> GetComputerReplics(int state)
    {
        List<string> replics = new List<string>();
        XmlNode enemyReplicsNode = GetUserReplicsNode(state, AI_REPLICA);

        foreach (XmlNode replica in enemyReplicsNode)
        {
            replics.Add(replica.InnerText);
        }

        return replics;
    }
    public List<PlayerReplica> GetPlayerReplics(int state)
    {
        List<PlayerReplica> replics = new List<PlayerReplica>();
        XmlNode platerReplicsNode = GetUserReplicsNode(state, PLAYER_REPLICA);

        foreach (XmlNode node in platerReplicsNode)
        {
            string toButton = node.Attributes.GetNamedItem(TO_BUTTON_ATRIBUTE).InnerText;
            string toSend = node.InnerText;
            int nextState = int.Parse(node.Attributes.GetNamedItem(NEXT_ATRIBUTE).InnerText);

            PlayerReplica replica = new PlayerReplica(toButton, toSend, nextState);
            replics.Add(replica);
        }

        return replics;
    }
    XmlNode GetUserReplicsNode(int state, string user)
    {
        string stateName = STATE_PATTERN + state.ToString();
        XmlNode stateNode = m_xml.DocumentElement.SelectSingleNode(stateName);
        XmlNode userNode = stateNode.SelectSingleNode(user);

        return userNode;
    }

}