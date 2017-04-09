using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public struct UserReplica
{
    public UserReplica(string toButton, string toSend, int nextState, float coldown = 0)
    {
        this.toButton = toButton;
        this.toSend = toSend;
        this.nextState = nextState;
        this.waitColdown = coldown;
    }

    public readonly string toButton;
    public readonly string toSend;
    public readonly int nextState;
    public readonly float waitColdown;
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

    const string COLDOWN_PATTERN = "coldown";
    const string STATE_PATTERN = "state_";
    const string NEXT_ATRIBUTE = "next";
    const string TO_BUTTON_ATRIBUTE = "toButton";
    const string AI_REPLICA = "computer";
    const string PLAYER_REPLICA = "player";

    public List<UserReplica> GetComputerReplics(int state)
    {
        List<UserReplica> replics = new List<UserReplica>();
        XmlNode enemyReplicsNode = GetUserReplicsNode(state, AI_REPLICA);

        foreach (XmlNode node in enemyReplicsNode)
        {
            string toSend = node.InnerText;

            float coldown = 0;
            string coldownStr = node.Attributes.GetNamedItem(COLDOWN_PATTERN).InnerText;
            if (coldownStr != "")
            {
                coldown = float.Parse(coldownStr);
            }

            UserReplica computerReplica = new UserReplica("", toSend, 0, coldown);
            replics.Add(computerReplica);
        }

        return replics;
    }
    public List<UserReplica> GetPlayerReplics(int state)
    {
        List<UserReplica> replics = new List<UserReplica>();
        XmlNode platerReplicsNode = GetUserReplicsNode(state, PLAYER_REPLICA);

        foreach (XmlNode node in platerReplicsNode)
        {
            string toButton = node.Attributes.GetNamedItem(TO_BUTTON_ATRIBUTE).InnerText;
            string toSend = node.InnerText;
            int nextState = int.Parse(node.Attributes.GetNamedItem(NEXT_ATRIBUTE).InnerText);

            UserReplica replica = new UserReplica(toButton, toSend, nextState);
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