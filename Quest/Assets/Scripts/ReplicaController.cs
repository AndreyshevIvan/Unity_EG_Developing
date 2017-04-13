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

public class ReplicaController
{
    public ReplicaController(string chatName)
    {
        m_document = new XmlDocument();
        TextAsset xmlText = Resources.Load(DataManager.xmlPath + chatName) as TextAsset;
        m_document.LoadXml(xmlText.text);
    }

    XmlDocument m_document;

    const string COLDOWN_PATTERN = "coldown";
    const string STATE_PATTERN = "state_";
    const string NEXT_ATRIBUTE = "next";
    const string TO_BUTTON_ATRIBUTE = "toButton";
    const string AI_REPLICA = "computer";
    const string PLAYER_REPLICA = "player";
    const string KEYS_KEY = "keys";
    const char KEYS_SEPARATOR = ',';

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
    public List<string> GetStateKey(int state)
    {
        List<string> keys = new List<string>();
        string stateName = STATE_PATTERN + state.ToString();
        XmlNode stateNode = m_document.DocumentElement.SelectSingleNode(stateName);
        XmlNode keysNode = stateNode.Attributes.GetNamedItem(KEYS_KEY);

        if (keysNode != null)
        {
            string keysStr = keysNode.InnerText;

            if (keysStr != "")
            {
                string[] allKeys = keysStr.Split(KEYS_SEPARATOR);

                foreach (string key in allKeys)
                {
                    keys.Add(key);
                }
            }
        }

        return keys;
    }
    XmlNode GetUserReplicsNode(int state, string user)
    {
        string stateName = STATE_PATTERN + state.ToString();
        XmlNode stateNode = m_document.DocumentElement.SelectSingleNode(stateName);
        XmlNode userNode = stateNode.SelectSingleNode(user);

        return userNode;
    }

}