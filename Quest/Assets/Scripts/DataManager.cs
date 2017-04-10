using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class DataManager
{
    readonly static string resourcesPath = Application.dataPath + "/Resources/";
    readonly static string saveDataPath = Application.persistentDataPath;
    public readonly static string historyPath = resourcesPath + "Histories/";
    public readonly static string xmlPath = "xml/";
    public readonly static string saveFileType = ".txt";
    public readonly static string replicsFileType = ".xml";
    readonly static string historyNamePattern = "History_";
    readonly static string playerKey = "GameStarted";
    readonly static string bookmarksKey = "bookmarks";

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
        FileStream stream = new FileStream(saveDataPath + fileName, FileMode.Create);

        bFormatter.Serialize(stream, history);
        stream.Close();
    }

    public static History LoadHistory(string historyName)
    {
        string fileName = historyNamePattern + historyName + saveFileType;
        History history = new History(historyName);

        if (File.Exists(saveDataPath + fileName))
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(saveDataPath + fileName, FileMode.Open);

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

    public static bool IsGameBeenStarted()
    {
        return PlayerPrefs.GetString(playerKey, "") != "";
    }

    public static void StartGame(string playerName)
    {
        PlayerPrefs.SetString(playerKey, playerName);
        PlayerPrefs.Save();
    }

    public static string GetPlayerName()
    {
        return PlayerPrefs.GetString(playerKey, playerKey);
    }

    public static void ResetAll(string historyName)
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
