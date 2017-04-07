using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class DataManager
{
    readonly static string m_resourcesPath = Application.dataPath + "/Resources/";
    readonly static string m_historyPath = "Histories/";
    readonly static string m_fileType = ".txt";
    readonly static string m_historyNamePattern = "History_";

    public static void SaveHistory(History history)
    {
        string fileName = m_historyNamePattern + history.GetName() + m_fileType;

        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(m_resourcesPath + m_historyPath + fileName, FileMode.Create);

        bFormatter.Serialize(stream, history);
        stream.Close();
    }

    public static History LoadHistory(string historyName)
    {
        string fileName = m_historyNamePattern + historyName + m_fileType;
        History history = new History(historyName);

        if (File.Exists(m_resourcesPath + m_historyPath + fileName))
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(m_resourcesPath + m_historyPath + fileName, FileMode.Open);

            history = bFormatter.Deserialize(stream) as History;
            stream.Close();
            return history;
        }
        else
        {
            SaveHistory(history);
        }

        return history;
    }
}
