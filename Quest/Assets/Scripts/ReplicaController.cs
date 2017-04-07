using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using UnityEngine;

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
    const string AI_REPLICA = "computer";
    const string PLAYER_REPLICA = "player";

    public List<string> GetComputerReplics(int state)
    {
        return GetUserReplics(state, AI_REPLICA);
    }
    public List<string> GetPlayerReplics(int state)
    {
        return GetUserReplics(state, PLAYER_REPLICA);
    }
    List<string> GetUserReplics(int state, string user)
    {
        List<string> replics = new List<string>();
        string stateName = STATE_PATTERN + state.ToString();

        XmlNode stateNode = m_xml.DocumentElement.SelectSingleNode(stateName);
        XmlNode enemyNode = stateNode.SelectSingleNode(user);

        foreach (XmlNode node in enemyNode)
        {
            replics.Add(node.InnerText);
        }

        return replics;
    }

}