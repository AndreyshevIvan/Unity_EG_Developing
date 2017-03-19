using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController
{
    const int m_statNodesCount = 3;

    const string m_mapIndexKey = "MapIndex";
    const string m_usernameKey = "Username";
    const string m_bestScore = "BestScore";
    const string m_sound = "Sound";

    const string m_playerStat = "_player";
    const string m_scoreStat = "_score";

    const string m_normalMapKey = "_Normal";
    const string m_largeMapKey = "_Large";
    const string m_extraLarge = "_ExtraLarge";

    const string m_firstStatKey = "_first";
    const string m_secondStatKey = "_second";
    const string m_thirdStatKey = "_third";

    string mapKey
    {
        get
        {
            return IndexToKey(GetMapIndex());
        }
    }
    string firstStat
    {
        get
        {
            string key = m_bestScore + mapKey + m_firstStatKey;

            return key;
        }
    }
    string secondStat
    {
        get
        {
            string key = m_bestScore + mapKey + m_secondStatKey;

            return key;
        }
    }
    string thirdStat
    {
        get
        {
            string key = m_bestScore + mapKey + m_thirdStatKey;

            return key;
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    public int GetMapIndex()
    {
        int index = PlayerPrefs.GetInt(m_mapIndexKey, 0);

        return index;
    }
    public string GetUsername()
    {
        string username = PlayerPrefs.GetString(m_usernameKey, "@username");

        return username;
    }
    public int GetMapBestScore()
    {
        int score = PlayerPrefs.GetInt(firstStat + m_scoreStat, 0);

        return score;
    }
    public int[] GetBestScores(int mapIndex)
    {
        int[] scores = new int[m_statNodesCount];
        string mapType = IndexToKey(mapIndex);

        scores[0] = PlayerPrefs.GetInt(m_bestScore + mapType + m_firstStatKey + m_scoreStat, 0);
        scores[1] = PlayerPrefs.GetInt(m_bestScore + mapType + m_secondStatKey + m_scoreStat, 0);
        scores[2] = PlayerPrefs.GetInt(m_bestScore + mapType + m_thirdStatKey + m_scoreStat, 0);

        return scores;
    }
    public string[] GetBestPlayers(int mapIndex)
    {
        string[] players = new string[m_statNodesCount];
        string mapType = IndexToKey(mapIndex);

        players[0] = PlayerPrefs.GetString(m_bestScore + mapType + m_firstStatKey + m_playerStat, "");
        players[1] = PlayerPrefs.GetString(m_bestScore + mapType + m_secondStatKey + m_playerStat, "");
        players[2] = PlayerPrefs.GetString(m_bestScore + mapType + m_thirdStatKey + m_playerStat, "");

        return players;
    }
    public bool IsSoundActive()
    {
        bool isActive = false;

        if (PlayerPrefs.GetInt(m_sound, 1) == 1)
        {
            isActive = true;
        }

        return isActive;
    }

    public void SetMapIndex(int mapIndex)
    {
        PlayerPrefs.SetInt(m_mapIndexKey, mapIndex);
    }
    public void SetUsername(string username)
    {
        PlayerPrefs.SetString(m_usernameKey, username);
    }
    public void SetBestScore(int mapIndex, int newScore, string newPlayer)
    {
        if (newScore != 0)
        {
            int[] scores = GetBestScores(mapIndex);
            string[] players = GetBestPlayers(mapIndex);

            bool isPasteNew = false;
            List<string> newBestPlayers = new List<string>();
            List<int> newBestScores = new List<int>();

            for (int i = 0; i < m_statNodesCount; i++)
            {
                if (scores[i] <= newScore && !isPasteNew)
                {
                    newBestPlayers.Add(newPlayer);
                    newBestScores.Add(newScore);
                    isPasteNew = true;
                }

                newBestPlayers.Add(players[i]);
                newBestScores.Add(scores[i]);
            }

            PlayerPrefs.SetInt(firstStat + m_scoreStat, newBestScores[0]);
            PlayerPrefs.SetInt(secondStat + m_scoreStat, newBestScores[1]);
            PlayerPrefs.SetInt(thirdStat + m_scoreStat, newBestScores[2]);

            PlayerPrefs.SetString(firstStat + m_playerStat, newBestPlayers[0]);
            PlayerPrefs.SetString(secondStat + m_playerStat, newBestPlayers[1]);
            PlayerPrefs.SetString(thirdStat + m_playerStat, newBestPlayers[2]);
        }
    }
    public void SetSoundActive(bool isMusicActive)
    {
        int soundActive = (isMusicActive) ? 1 : 0;

        PlayerPrefs.SetInt(m_sound, soundActive);
    }

    string IndexToKey(int mapIndex)
    {
        string mapKey = m_normalMapKey;

        switch (mapIndex)
        {
            case 0:
                mapKey = m_normalMapKey;
                break;
            case 1:
                mapKey = m_largeMapKey;
                break;
            case 2:
                mapKey = m_extraLarge;
                break;
        }

        return mapKey;
    }

    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }
}
