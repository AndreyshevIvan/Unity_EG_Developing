using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class DataManager
{
    readonly static string resourcesPath = Application.dataPath + "/Resources/";
    public readonly static string historyPath = resourcesPath + "Histories/";
    public readonly static string xmlPath = resourcesPath + "xml/";
    public readonly static string saveFileType = ".txt";
    public readonly static string replicsFileType = ".xml";
    readonly static string historyNamePattern = "History_";

    public static string playerName
    {
        get
        {
            return PlayerPrefs.GetString("playerName", "player");
        }
        set
        {
            PlayerPrefs.SetString("playerName", value);
        }
    }

    public static void SaveHistory(History history)
    {
        string fileName = historyNamePattern + history.GetName() + saveFileType;

        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(historyPath + fileName, FileMode.Create);

        bFormatter.Serialize(stream, history);
        stream.Close();
    }

    public static History LoadHistory(string historyName)
    {
        string fileName = historyNamePattern + historyName + saveFileType;
        History history = new History(historyName);

        if (File.Exists(historyPath + fileName))
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(historyPath + fileName, FileMode.Open);

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
